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
    public interface IClassGroupService : IBaseService
    {
        SearchResponse<ClassGroupModel> SearchClassGroup(SearchRequest request);
        SearchResponse<ClassGroupModel> GetSemesterClassGroup(Guid semesterId);
        ClassGroupModel GetClassGroup(ClassGroupModel model);
        ClassGroupModel CreateOrUpdate(ClassGroupModel model);
    }
}
