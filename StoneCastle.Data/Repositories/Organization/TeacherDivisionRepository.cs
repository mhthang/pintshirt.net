using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;

namespace StoneCastle.Organization.Repositories
{
    public class TeacherDivisionRepository : Repository<Organization.Models.TeacherDivision, System.Guid>, ITeacherDivisionRepository
    {
        public TeacherDivisionRepository(ISCDataContext context) : base(context)
        {
        }
     
        public TeacherDivision GetByTeacherId(Guid teacherId)
        {
            if (teacherId == null)
                throw new System.ArgumentNullException("TeacherId");

            TeacherDivision teacherDivision = this.GetAll().Where(x => x.TeacherId == teacherId).FirstOrDefault();

            return teacherDivision;
        }

        public Timetable CreateTeacherTimetable(Guid id, int shiftPerDay, int slotPerShift)
        {
            TeacherDivision teacherDivision = this.GetById(id);

            if (teacherDivision == null)
                throw new InvalidOperationException($"Teacher Division ({id}) does not exist.");

            Timetable tt = new Timetable()
            {
                Id = Guid.NewGuid(),
                ShiftPerDay = shiftPerDay,
                SlotPerShift = slotPerShift,
                HighlightColor = Commons.Ultility.GetHighlightColor(new Random())
            };

            teacherDivision.TimetableId = tt.Id;
            this.DataContext.Insert<Timetable>(tt);
            this.DataContext.Update<TeacherDivision, Guid>(teacherDivision, x=>x.TeacherId);
            return tt;
        }
    }
}
