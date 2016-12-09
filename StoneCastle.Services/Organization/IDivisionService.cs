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
    public interface IDivisionService : IBaseService
    {
        SearchResponse<DivisionModel> SearchDivisions(SearchRequest request);
        SearchResponse<DivisionModel> GetSemesterDivision(Guid semesterId);

        DivisionModel GetDivision(DivisionModel model);
        DivisionModel CreateOrUpdate(DivisionModel model);
    }
}
