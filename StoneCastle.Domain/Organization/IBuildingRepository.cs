using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IBuildingRepository : IRepository<Organization.Models.Building, System.Guid>
    {
        List<Models.Building> SearchBuilding(string filter, int pageIndex, int pageSize);
        List<Models.Building> SearchSemesterBuilding(Guid semesterId, string filter, int pageIndex, int pageSize);
        List<Models.Building> GetSemesterBuilding(Guid semesterId);

        Models.Building CreateBuilding(Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        Models.Building UpdateBuidling(Guid id, Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);

        int CountSemesterBuilding(Guid semesterId);
    }
}
