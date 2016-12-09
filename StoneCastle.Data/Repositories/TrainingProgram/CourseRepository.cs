using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using StoneCastle.Schedule.Models;
using StoneCastle.TrainingProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.TrainingProgram.Repositories
{
    public class CourseRepository : Repository<Course, System.Guid>, ICourseRepository
    {
        public CourseRepository(ISCDataContext context) : base(context)
        {
        }

        public IEnumerable<Course> GetCourseSubjectByTrainingProgram(Guid programId)
        {
            return this.GetAll().Where(x => x.TrainingProgramId == programId);
        }

        public List<Models.Course> SearchCourseSubject(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Course> SearchSemesterCourseSubject(Guid semesterId, string filter, int pageIndex, int pageSize, ref int total)
        {
            var courses = this.GetAll().Where(x => (x.TrainingProgram.SemesterId == semesterId))
                .OrderBy(x => x.Subject.Name);

            total = courses.Count();
            return courses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public Models.Course CreateCourseSubject(Guid programId, Guid subjectId, string name, string shortName, int totalSections, int sectionPerWeek, bool isTeachingByHomeroomTeacher, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (programId == null || programId == System.Guid.Empty)
                throw new ArgumentNullException("programId");

            Models.Course course = new Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgramId = programId,
                SubjectId = subjectId,
                TotalSection = totalSections,
                SectionPerWeek = sectionPerWeek,
                IsTeachingByHomeroomTeacher = isTeachingByHomeroomTeacher,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Course>(course);

            return course;
        }

        public Models.Course UpdateCourseSubject(Guid id, Guid subjectId, string name, string shortName, int totalSections, int sectionPerWeek, bool isTeachingByHomeroomTeacher, bool isActive)
        {
            Models.Course course = this.GetById(id);

            if (course == null)
                throw new InvalidOperationException($"Course Subject ({id}) does not exist.");

            course.SubjectId = subjectId;
            course.TotalSection = totalSections;
            course.SectionPerWeek = sectionPerWeek;
            course.IsTeachingByHomeroomTeacher = isTeachingByHomeroomTeacher;
            course.IsActive = isActive;

            this.DataContext.Update<Models.Course, Guid>(course, x=>x.TotalSection, x=>x.SectionPerWeek, x=>x.IsTeachingByHomeroomTeacher, x => x.IsActive);

            return course;
        }

        public Timetable GetTimetable(Guid courseSubjectId)
        {
            if (courseSubjectId == null)
                throw new ArgumentNullException("courseSubjectId");

            Timetable tt = this.GetAll().Where(x => x.Id == courseSubjectId).Select(x => x.Timetable).FirstOrDefault();

            return tt;
        }

        public Timetable CreateTimetable(Guid courseSubjectId, int shiftPerDay, int slotPerShift)
        {
            if (courseSubjectId == null)
                throw new ArgumentNullException("courseSubjectId");

            Timetable tt = this.GetAll().Where(x => x.Id == courseSubjectId).Select(x => x.Timetable).FirstOrDefault();

            if (tt == null)
            {
                tt = new Timetable()
                {
                    Id = Guid.NewGuid(),
                    ShiftPerDay = shiftPerDay,
                    SlotPerShift = slotPerShift,
                    HighlightColor = Commons.Ultility.GetHighlightColor(new Random())
                };

                Course courseSubject = this.GetById(courseSubjectId);
                if (courseSubject == null)
                    throw new InvalidOperationException($"course Subject ({courseSubjectId}) does not exist.");

                courseSubject.TimetableId = tt.Id;
                this.DataContext.Insert<Timetable>(tt);
                this.DataContext.Update<Course, Guid>(courseSubject, x => x.TimetableId);
            }

            return tt;
        }
    }
}
