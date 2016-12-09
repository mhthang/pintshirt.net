using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IClassRoomRepository : IRepository<Organization.Models.ClassRoom, System.Guid>
    {
        List<Models.ClassRoom> SearchClassRoom(string filter, int pageIndex, int pageSize);
        List<Models.ClassRoom> SearchSemesterClasses(Guid semesterId, Guid? classGroupId, string filter, int pageIndex, int pageSize, ref int total);

        Models.ClassRoom CreateClassRoom(Guid classGroupId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        Models.ClassRoom UpdateClassRoom(Guid id, Guid classGroupId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        bool UpdateHomeroomTeacher(Guid classRoomId, Guid? teacherId);

        int CountSemesterClassRoom(Guid semesterId);
    }
}
