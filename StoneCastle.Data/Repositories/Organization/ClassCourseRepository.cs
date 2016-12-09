using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using StoneCastle.Organization.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Schedule.Models;

namespace StoneCastle.Organization.Repositories
{
    public class ClassCourseRepository : Repository<ClassCourse, System.Guid>, IClassCourseRepository
    {
        public ClassCourseRepository(ISCDataContext context) : base(context)
        {
        }

        public IEnumerable<ClassCourse> GetCoursesByClassRoom(Guid classRoomId)
        {
            return this.GetAll().Where(x => x.ClassRoomId == classRoomId);
        }

        public List<Models.ClassCourse> SearchCourse(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.ClassCourse> SearchSemesterCourse(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x=>x.ClassRoom.ClassGroup.SemesterId == semesterId) .ToList();
        }

        public Models.ClassCourse CreateCourse(Guid? classRoomId, Guid? subjectId, Guid? teacherId, Guid? roomId, bool isActive)
        {
            Models.ClassCourse course = new Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoomId = classRoomId,
                CourseId = subjectId,
                TeacherId = teacherId != null && teacherId != Guid.Empty? teacherId : (Guid?)null,
                RoomId = roomId != null && roomId != Guid.Empty ? roomId : (Guid?)null,                
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.ClassCourse>(course);

            return course;
        }

        public Models.ClassCourse UpdateCourse(Guid id, Guid? classRoomId, Guid? subjectId, Guid? teacherId, Guid? roomId, bool isActive)
        {
            Models.ClassCourse course = this.GetById(id);

            if (course == null)
                throw new InvalidOperationException($"Course ({id}) does not exist.");

            course.ClassRoomId = classRoomId;
            course.CourseId = subjectId;
            course.TeacherId = teacherId != null && teacherId != Guid.Empty ? teacherId : (Guid?)null;
            if (roomId != null && roomId != Guid.Empty)
                course.RoomId = roomId;
            else
                course.RoomId = (Guid?)null;

            course.IsActive = isActive;

            this.DataContext.Update<Models.ClassCourse, Guid>(course, x=>x.ClassRoomId, x=>x.CourseId, x=>x.TeacherId, x=>x.RoomId, x => x.IsActive);

            return course;
        }

        public Timetable GetTimetable(Guid courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException("courseId");

            Timetable tt = this.GetAll().Where(x => x.Id == courseId).Select(x => x.Timetable).FirstOrDefault();

            return tt;
        }

        public Timetable CreateTimetable(Guid courseId, int shiftPerDay, int slotPerShift)
        {
            if (courseId == null)
                throw new ArgumentNullException("courseId");

            Timetable tt = this.GetAll().Where(x => x.Id == courseId).Select(x => x.Timetable).FirstOrDefault();

            if (tt == null)
            {
                tt = new Timetable()
                {
                    Id = Guid.NewGuid(),
                    ShiftPerDay = shiftPerDay,
                    SlotPerShift = slotPerShift,
                    HighlightColor = Commons.Ultility.GetHighlightColor(new Random())
                };

                ClassCourse course = this.GetById(courseId);
                if (course == null)
                    throw new InvalidOperationException($"Course ({courseId}) does not exist.");

                course.TimetableId = tt.Id;
                this.DataContext.Insert<Timetable>(tt);
                this.DataContext.Update<Organization.Models.ClassCourse, Guid>(course, x => x.TimetableId);
            }

            return tt;
        }

        public Guid AddCourse(Guid classRoomId, Guid courseSubjectId)
        {
            Models.ClassCourse course = new Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoomId = classRoomId,
                CourseId = courseSubjectId,
                IsActive = true,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.ClassCourse>(course);

            return course.Id;
        }

        public int CountSemesterCourse(Guid semesterId)
        {
            List<Guid> groups = this.DataContext.Get<Models.ClassGroup>().Where(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList();
            List<Guid> classRooms = this.DataContext.Get<Models.ClassRoom>().Where(x => x.ClassGroupId.HasValue && groups.Contains(x.ClassGroupId.Value) && x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList();

            return this.GetAll().Count(x => x.ClassRoomId.HasValue && classRooms.Contains(x.ClassRoomId.Value) && x.IsActive && !x.IsDeleted);
        }
    }
}
