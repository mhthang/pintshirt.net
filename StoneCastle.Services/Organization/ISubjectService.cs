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
    public interface ISubjectService : IBaseService
    {
        SearchResponse<SubjectModel> SearchSubject(SearchRequest request);
        SearchResponse<SubjectModel> SearchSemesterSubject(SearchRequest request);

        SubjectModel GetSubject(SubjectModel model);
        SubjectModel CreateOrUpdate(SubjectModel model);
    }
}
