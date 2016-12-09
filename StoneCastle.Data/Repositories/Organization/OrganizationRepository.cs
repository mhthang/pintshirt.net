using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StoneCastle.Organization.Repositories
{
    public class OrganizationRepository : Repository<Organization.Models.Organization, System.Guid>, IOrganizationRepository
    {
        public OrganizationRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Organization> SearchOrganization(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Organization> GetUserOrganization(String userId)
        {
            return this.GetAll().ToList();
        }

        public Models.Organization CreateOrganization(string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Models.Organization organization = new Models.Organization()
            {
                Id = Guid.NewGuid(),                
                Name = name,
                ShortName = shortName,
                HighlightColor = HighlightColor,
                LogoUrl = logoUrl,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Organization>(organization);

            return organization;
        }

        public Models.Organization UpdateOrganization(Guid id, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.Organization organization = this.GetById(id);

            if (organization == null)
                throw new InvalidOperationException($"Organization ({id}) does not exist.");

            organization.Name = name;
            organization.ShortName = shortName;
            organization.HighlightColor = HighlightColor;
            organization.LogoUrl = logoUrl;
            organization.IsActive = isActive;

            this.DataContext.Update<Models.Organization, Guid>(organization, x => x.Name, x=>x.HighlightColor, x => x.LogoUrl, x => x.IsActive);

            return organization;
        }
    }
}
