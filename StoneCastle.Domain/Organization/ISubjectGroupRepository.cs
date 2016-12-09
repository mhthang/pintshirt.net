using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface ISubjectGroupRepository : IRepository<Organization.Models.SubjectGroup, System.Guid>
    {
        List<Models.SubjectGroup> SearchSubjectGroup(string filter, int pageIndex, int pageSize);
        List<Models.SubjectGroup> SearchSemesterSubjectGroup(Guid semesterId, string filter, int pageIndex, int pageSize);
        List<Models.SubjectGroup> GetSemesterSubjectGroup(Guid semesterId);

        Models.SubjectGroup CreateSubjectGroup(Guid semesterId, string name, string shortName, string HighlightColor, bool isActive);
        Models.SubjectGroup UpdateSubjectGroup(Guid id, string name, string shortName, string HighlightColor, bool isActive);

        int CountSemesterSubjectGroup(Guid semesterId);
    }
}
