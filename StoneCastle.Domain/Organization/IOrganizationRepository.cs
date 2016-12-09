using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IOrganizationRepository : IRepository<Organization.Models.Organization, System.Guid>
    {
        List<Models.Organization> SearchOrganization(string filter, int pageIndex, int pageSize);

        Models.Organization CreateOrganization(string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        Models.Organization UpdateOrganization(Guid id, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);

        List<Models.Organization> GetUserOrganization(String userId);
    }
}
