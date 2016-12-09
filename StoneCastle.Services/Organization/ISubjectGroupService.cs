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
    public interface ISubjectGroupService : IBaseService
    {
        SearchResponse<SubjectGroupModel> SearchSubjectGroup(SearchRequest request);
        SearchResponse<SubjectGroupModel> SearchSemesterSubjectGroup(SearchRequest request);
        List<SubjectGroupModel> GetSemesterSubjectByGroup(Guid semesterId);

        SearchResponse<SubjectGroupModel> GetSemesterSubjectGroup(Guid semesterId);
        SubjectGroupModel GetSubjectGroup(SubjectGroupModel model);
        SubjectGroupModel CreateOrUpdate(SubjectGroupModel model);
    }
}
