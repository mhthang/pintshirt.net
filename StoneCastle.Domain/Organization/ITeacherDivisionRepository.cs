using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface ITeacherDivisionRepository : IRepository<Organization.Models.TeacherDivision, System.Guid>
    {
        Organization.Models.TeacherDivision GetByTeacherId(Guid teacherId);

        Timetable CreateTeacherTimetable(Guid id, int shiftPerDay, int slotPerShift);
    }
}
