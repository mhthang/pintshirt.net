using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Linq;
using System.Collections.Generic;
using System;

namespace StoneCastle.Organization.Repositories
{
    public class BuildingRepository : Repository<Organization.Models.Building, System.Guid>, IBuildingRepository
    {
        public BuildingRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Building> SearchBuilding(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Building> SearchSemesterBuilding(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x => x.SemesterId == semesterId && !x.IsDeleted).ToList();
        }

        public List<Models.Building> GetSemesterBuilding(Guid semesterId)
        {
            return this.GetAll().Where(x=>x.SemesterId == semesterId && !x.IsDeleted).ToList();
        }

        public Models.Building CreateBuilding(Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (semesterId == null || semesterId == System.Guid.Empty)
                throw new ArgumentNullException("semesterId");

            Models.Building center = new Models.Building()
            {
                Id = Guid.NewGuid(),
                SemesterId = semesterId,
                Name = name,
                Code = shortName,
                HighlightColor = HighlightColor,
                LogoUrl = logoUrl,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Building>(center);

            return center;
        }

        public Models.Building UpdateBuidling(Guid id, Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.Building center = this.GetById(id);

            if (center == null)
                throw new InvalidOperationException($"Building ({id}) does not exist.");

            center.Name = name;
            center.Code = shortName;
            center.HighlightColor = HighlightColor;
            center.LogoUrl = logoUrl;
            center.IsActive = isActive;

            this.DataContext.Update<Models.Building, Guid>(center, x=>x.SemesterId, x => x.Name, x=>x.Code, x=>x.HighlightColor, x => x.LogoUrl, x => x.IsActive);

            return center;
        }

        public int CountSemesterBuilding(Guid semesterId)
        {
            return this.GetAll().Count(x => x.SemesterId == semesterId && x.IsActive == true && x.IsDeleted == false);
        }
    }
}

