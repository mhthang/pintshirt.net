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
    public interface IOrganizationService : IBaseService
    {
        SearchResponse<OrganizationModel> SearchOrganization(SearchRequest request);
        SearchResponse<UserOrganizationModel> GetUserOrganizations(String userId);
        OrganizationModel GetOrganization(OrganizationModel model);
        OrganizationModel CreateOrUpdate(OrganizationModel model);
    }
}
