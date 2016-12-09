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
    public interface ISemesterService : IBaseService
    {
        SearchResponse<SemesterModel> GetAllSemesters();
        SearchResponse<SemesterModel> SearchSemester(SearchRequest request);
        SemesterModel GetSemester(SemesterModel model);
        SemesterModel GetSemesterWithOrganization(SemesterModel model); 
        SemesterModel CreateOrUpdate(SemesterModel model);

        SemesterSummaryModel GetSummary(Guid semesterId);
    }
}
