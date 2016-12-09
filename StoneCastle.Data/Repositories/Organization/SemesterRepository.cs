using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Schedule.Models;
using StoneCastle.Organization.Models;

namespace StoneCastle.Organization.Repositories
{
    public class SemesterRepository : Repository<Organization.Models.Semester, System.Guid>, ISemesterRepository
    {
        public SemesterRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Semester> GetAllSemesters()
        {
            return this.GetAll().ToList();
        }

        public List<Models.Semester> SearchSemester(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Semester> SearchSemesterFromOrg(Guid orgId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x=>x.OrganizationId == orgId && !x.IsDeleted).ToList();
        }

        public Models.Semester CreateSemester(Guid organizationId, string name, string shortName, string HighlightColor, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (organizationId == null || organizationId == System.Guid.Empty)
                throw new ArgumentNullException("organizationId");

            Models.Semester semester = new Models.Semester()
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                Name = name,
                ShortName = shortName,
                HighlightColor = HighlightColor,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Semester>(semester);

            return this.DataContext.Insert<Models.Semester>(semester);
            ;
        }

        public Models.Semester UpdateSemester(Guid id, string name, string shortName, string HighlightColor, bool isActive)
        {
            Models.Semester semester = this.GetById(id);

            if (semester == null)
                throw new InvalidOperationException($"Semester ({id}) does not exist.");

            semester.Name = name;
            semester.ShortName = shortName;
            semester.HighlightColor = HighlightColor;
            semester.IsActive = isActive;

            this.DataContext.Update<Models.Semester, Guid>(semester, x => x.Name, x=>x.ShortName, x => x.HighlightColor, x => x.IsActive);

            return semester;
        }

        public Timetable GetTimetable(Guid semesterId)
        {
            if (semesterId == null)
                throw new ArgumentNullException("semesterId");

            Timetable tt = this.GetAll().Where(x => x.Id == semesterId).Select(x => x.Timetable).FirstOrDefault();            

            return tt;
        }

        public Timetable CreateTimetable(Guid semesterId, int shiftPerDay, int slotPerShift)
        {
            if (semesterId == null)
                throw new ArgumentNullException("semesterId");

            Timetable tt = this.GetAll().Where(x => x.Id == semesterId).Select(x => x.Timetable).FirstOrDefault();

            if (tt == null)
            {
                tt = new Timetable()
                {
                    Id = Guid.NewGuid(),
                    ShiftPerDay = shiftPerDay,
                    SlotPerShift = slotPerShift,
                    HighlightColor = Commons.Ultility.GetHighlightColor(new Random())
                };

                Semester semester = this.GetById(semesterId);
                if (semester == null)
                    throw new InvalidOperationException($"Semester ({semesterId}) does not exist.");

                semester.TimetableId = tt.Id;
                this.DataContext.Insert<Timetable>(tt);
                this.DataContext.Update<Organization.Models.Semester, Guid>(semester, x => x.TimetableId);
            }

            return tt;
        }
    }
}
