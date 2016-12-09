using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StoneCastle.Organization.Repositories
{
    public class DivisionRepository : Repository<Organization.Models.Division, System.Guid>, IDivisionRepository
    {
        public DivisionRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Division> SearchDivision(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Division> SearchSemesterDivision(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x => x.SemesterId == semesterId && x.IsDeleted == false).ToList();
        }

        public List<Models.Division> GetSemesterDivision(Guid semesterId)
        {
            return this.GetAll().Where(x=>x.SemesterId == semesterId && x.IsDeleted == false).ToList();
        }

        public Models.Division CreateDivision(string name, Guid semesterId, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if(semesterId == null || semesterId == System.Guid.Empty)
                throw new ArgumentNullException("semesterId");

            Models.Division division = new Models.Division()
            {
                Id = Guid.NewGuid(),
                SemesterId = semesterId,
                Name = name,
                LogoUrl = logoUrl,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Division>(division);

            return division;
        }

        public Models.Division UpdateDivision(Guid id, string name, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.Division division = this.GetById(id);

            if (division == null)
                throw new InvalidOperationException($"Division ({id}) does not exist.");

            division.Name = name;
            division.LogoUrl = logoUrl;
            division.IsActive = isActive;

            this.DataContext.Update<Models.Division, Guid>(division, x => x.Name, x => x.LogoUrl, x => x.IsActive);

            return division;
        }

        public int CountSemesterDivision(Guid semesterId)
        {
            return this.GetAll().Count(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted);
        }
    }
}
