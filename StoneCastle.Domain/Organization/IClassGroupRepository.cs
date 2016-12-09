using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IClassGroupRepository : IRepository<ClassGroup, System.Guid>
    {
        IEnumerable<ClassGroup> GetSemesterClassGroups(Guid semesterId);

        List<Models.ClassGroup> SearchClassGroup(string filter, int pageIndex, int pageSize);
        List<Models.ClassGroup> SearchSemesterClassGroup(Guid semesterId, string filter, int pageIndex, int pageSize);
        long CountSemesterClassGroup(Guid semesterId, string filter);

        Models.ClassGroup CreateClassGroup(Guid semesterId, Guid programId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        Models.ClassGroup UpdateClassGroup(Guid id, Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
    }
}
