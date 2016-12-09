using StoneCastle.Account.Models;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Account.Repositories
{
    public class TeacherRepository : Repository<Account.Models.Teacher, System.Guid>, ITeacherRepository
    {
        public TeacherRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Teacher> SearchTeacher(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Teacher> SearchSemesterTeacher(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            //List<Guid> divisionIds = this.DataContext.Get<Division>().Where(x => x.SemesterId == semesterId).Select(x => x.Id).ToList();

            var teachers = this.DataContext.Get<Division>()
                             .Where(x => x.SemesterId == semesterId)
                             .SelectMany(x => x.TeacherDivisions)
                             .Distinct();

            return teachers.Select(x=>x.Teacher).ToList();
        }

        public List<Models.Teacher> GetSemesterTeacher(Guid semesterId)
        {
            var teachers = this.DataContext.Get<Division>()
                             .Where(x => x.SemesterId == semesterId)
                             .SelectMany(x => x.TeacherDivisions)
                             .Distinct();

            return teachers.Select(x => x.Teacher).ToList();
        }

        public List<Models.Teacher> GetAvailableSemesterHomeroomTeachers(Guid semesterId)
        {
            var teachers = this.GetSemesterTeacher(semesterId);
            var existHomeroomTeacherIds = this.DataContext.Get<ClassRoom>()
                             .Where(x => x.ClassGroup != null && x.ClassGroup.SemesterId == semesterId && x.TeacherId != null)
                             .Select(x => x.TeacherId);

            return teachers.Where(x => !existHomeroomTeacherIds.Contains(x.Id)).ToList();
        }

        public Models.Teacher CreateTeacher(Guid divisionId, string firstName, string lastName, string email, string phone, string highlightColor, bool isActive)
        {           
            Division division = this.DataContext.Get<Division>().Where(x => x.Id == divisionId).FirstOrDefault();

            if (division == null)
                throw new InvalidOperationException($"Division ({divisionId}) does not exist.");

            if (String.IsNullOrEmpty(highlightColor))
            {
                Random rand = new Random();
                highlightColor = Commons.Ultility.GetHighlightColor(rand);
            }
            Models.Profile profile = new Models.Profile()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                HighlightColor = highlightColor,                
                IsDeleted = false              
            };

            Models.Account account = new Models.Account()
            {
                Id = Guid.NewGuid(),
                Profile = profile,
                IsActive = isActive,
                IsDeleted = false
            };

            Models.Teacher teacher = new Models.Teacher()
            {
                Id = Guid.NewGuid(),
                Account = account,                
                IsActive = isActive,
                IsDeleted = false,
            };

            TeacherDivision teacherDivision = new TeacherDivision()
            {
                Id = Guid.NewGuid(),
                TeacherId = teacher.Id,
                DivisionId = divisionId
            };

            teacher.TeacherDivisions.Add(teacherDivision);

            this.DataContext.Insert<Models.Profile>(profile);
            this.DataContext.Insert<Models.Account>(account);
            this.DataContext.Insert<Models.Teacher>(teacher);
            this.DataContext.Insert<TeacherDivision>(teacherDivision);

            return teacher;
        }

        public Models.Teacher UpdateTeacher(Guid id, Guid divisionId, string firstName, string lastName, string email, string phone, string highlightColor, bool isActive)
        {
            Models.Teacher teacher = this.GetById(id);

            if (teacher == null)
                throw new InvalidOperationException($"Teacher ({id}) does not exist.");

            teacher.IsActive = isActive;

            Division division = this.DataContext.Get<Division>().Where(x => x.Id == divisionId).FirstOrDefault();

            if (division == null)
                throw new InvalidOperationException($"Division ({divisionId}) does not exist.");

            if(teacher.TeacherDivisions.Count(x=>x.DivisionId == divisionId) <= 0)
            {
                // Remove existing
                teacher.TeacherDivisions.Clear();

                // Add
                TeacherDivision teacherDivision = new TeacherDivision()
                {
                    Id = Guid.NewGuid(),
                    TeacherId = teacher.Id,
                    DivisionId = division.Id
                };

                teacher.TeacherDivisions.Add(teacherDivision);
            }

            Models.Account account = this.DataContext.Get<Models.Account>().Where(x => x.Id == teacher.AccountId).FirstOrDefault();

            if (account == null)
                throw new InvalidOperationException($"Account ({teacher.AccountId}) does not exist.");

            account.IsActive = isActive;

            Models.Profile profile = this.DataContext.Get<Models.Profile>().Where(x => x.Id == account.ProfileId).FirstOrDefault();

            if (profile == null)
                throw new InvalidOperationException($"Profile ({account.ProfileId}) does not exist.");

            if (String.IsNullOrEmpty(highlightColor))
            {
                Random rand = new Random();
                highlightColor = Commons.Ultility.GetHighlightColor(rand);
            }
            profile.FirstName = firstName;
            profile.LastName = lastName;
            profile.Email = email;
            profile.Phone = phone;
            profile.HighlightColor = highlightColor;

            this.DataContext.Update<Models.Profile, Guid>(profile, x => x.FirstName, x => x.LastName, x=>x.Email, x=>x.Phone);
            this.DataContext.Update<Models.Account, Guid>(account, x => x.IsActive);
            this.DataContext.Update<Models.Teacher, Guid>(teacher, x => x.IsActive);

            return teacher;
        }

        public Timetable GetTeacherTimetable(Guid teacherId, Guid scheduleId)
        {
            Timetable tt = tt = this.DataContext.Get<TeacherDivision>().Where(x => x.TeacherId == teacherId).Select(x => x.Timetable).FirstOrDefault(); ;

            if (tt == null)
            {
                tt = new Timetable() {
                    ShiftPerDay = 2,
                    SlotPerShift = 5
                };
            }            

            List<Guid> timetableIds = this.DataContext.Get<ClassTimetable>().Where(x => x.SchedulingTableId == scheduleId).Select(x => x.TimetableId).ToList();

            List<CourseSection> teacherCourseSections = this.DataContext.Get<CourseSection>().Where(x => x.ClassCourse.TeacherId == teacherId && timetableIds.Contains(x.TimetableId)).ToList();

            foreach(CourseSection cs in teacherCourseSections)
            {
                tt.CourseSections.Add(cs);
            }

            return tt;
        }

        public int CountSemesterTeacher(Guid semesterId)
        {
            List<Guid> divisions = this.DataContext.Get<Organization.Models.Division>().Where(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList();
            List<Guid> divisionTeachers = this.DataContext.Get<Organization.Models.TeacherDivision>().Where(x => divisions.Contains(x.DivisionId)).Select(x => x.TeacherId).ToList();

            return this.GetAll().Count(x => divisionTeachers.Contains(x.Id) && x.IsActive && !x.IsDeleted);
        }
    }
}
