using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface ISemesterRepository : IRepository<Organization.Models.Semester, System.Guid>
    {
        List<Models.Semester> GetAllSemesters();

        List<Models.Semester> SearchSemester(string filter, int pageIndex, int pageSize);
        List<Models.Semester> SearchSemesterFromOrg(Guid orgId, string filter, int pageIndex, int pageSize);

        Models.Semester CreateSemester(Guid organizationId, string name, string shortName, string HighlightColor, bool isActive);
        Models.Semester UpdateSemester(Guid id, string name, string shortName, string HighlightColor, bool isActive);

        Timetable GetTimetable(Guid semesterId);
        Timetable CreateTimetable(Guid semesterId, int shiftPerDay, int slotPerShift);
    }
}
