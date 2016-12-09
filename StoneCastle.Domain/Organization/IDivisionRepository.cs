using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IDivisionRepository : IRepository<Organization.Models.Division, System.Guid>
    {
        List<Models.Division> SearchDivision(string filter, int pageIndex, int pageSize);
        List<Models.Division> GetSemesterDivision(Guid semesterId);
        List<Models.Division> SearchSemesterDivision(Guid semesterId, string filter, int pageIndex, int pageSize);

        Models.Division CreateDivision(string name, Guid semesterId, string HighlightColor, string logoUrl, bool isActive);
        Models.Division UpdateDivision(Guid id, string name, string HighlightColor, string logoUrl, bool isActive);

        int CountSemesterDivision(Guid semesterId);
    }
}
