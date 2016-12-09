using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface ISubjectRepository : IRepository<Organization.Models.Subject, System.Guid>
    {
        List<Models.Subject> SearchSubject(string filter, int pageIndex, int pageSize);
        List<Models.Subject> SearchSemesterSubject(Guid semesterId, Guid? subjectGroupId, string filter, int pageIndex, int pageSize);

        Models.Subject CreateSubject(string name, Guid subjectGroupId, string HighlightColor, bool isActive);
        Models.Subject UpdateSubject(Guid id, string name, Guid subjectGroupId, string HighlightColor, bool isActive);

        int CountSemesterSubject(Guid semesterId);
        long CountSemesterSubject(Guid semesterId, Guid? subjectGroupId, string filter);
    }
}
