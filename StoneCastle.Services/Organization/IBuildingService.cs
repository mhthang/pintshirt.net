using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Organization.Services
{
    public interface IBuildingService : IBaseService
    {
        SearchResponse<BuildingModel> SearchBuilding(SearchRequest request);
        SearchResponse<BuildingModel> SearchSemesterBuilding(SearchRequest request);
        SearchResponse<BuildingModel> GetSemesterBuilding(Guid semesterId);

        BuildingModel GetBuilding(BuildingModel model);
        BuildingModel CreateOrUpdate(BuildingModel model);
    }
}
