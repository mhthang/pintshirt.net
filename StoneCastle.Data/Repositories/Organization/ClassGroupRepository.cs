using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Organization.Repositories
{
    public class ClassGroupRepository : Repository<ClassGroup, System.Guid>, IClassGroupRepository
    {
        public ClassGroupRepository(ISCDataContext context) : base(context)
        {
        }

        public IEnumerable<ClassGroup> GetSemesterClassGroups(Guid semesterId)
        {
            return this.GetAll().Where(x => x.SemesterId == semesterId);
        }

        public List<Models.ClassGroup> SearchClassGroup(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.ClassGroup> SearchSemesterClassGroup(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            var classGroups = this.GetAll().Where(x => (x.SemesterId == semesterId) &&
                (string.IsNullOrWhiteSpace(filter) || x.Name.ToLower().Contains(filter.Trim().ToLower())))
                .OrderBy(x => x.Name)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return classGroups;
        }

        public long CountSemesterClassGroup(Guid semesterId, string filter)
        {
            var classGroups = this.GetAll().Count(x => (x.SemesterId == semesterId) &&
                (string.IsNullOrWhiteSpace(filter) || x.Name.ToLower().Contains(filter.Trim().ToLower())));               

            return classGroups;
        }

        public Models.ClassGroup CreateClassGroup(Guid semesterId, Guid programId, string name, string code, string HighlightColor, string logoUrl, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (semesterId == null || semesterId == System.Guid.Empty)
                throw new ArgumentNullException("semesterId");

            Models.ClassGroup cgroup = new Models.ClassGroup()
            {
                Id = Guid.NewGuid(),
                SemesterId = semesterId,
                TrainingProgramId = programId,
                Name = name,
                Code = code,
                HighlightColor = HighlightColor,
                LogoUrl = logoUrl,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.ClassGroup>(cgroup);

            return cgroup;
        }

        public Models.ClassGroup UpdateClassGroup(Guid id, Guid classGroupId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.ClassGroup cgroup = this.GetById(id);

            if (cgroup == null)
                throw new InvalidOperationException($"Class Group ({id}) does not exist.");

            cgroup.Name = name;
            cgroup.Code = shortName;
            cgroup.HighlightColor = HighlightColor;
            cgroup.LogoUrl = logoUrl;
            cgroup.IsActive = isActive;

            this.DataContext.Update<Models.ClassGroup, Guid>(cgroup, x=>x.SemesterId, x => x.Name, x=>x.Code, x=>x.HighlightColor, x => x.LogoUrl, x => x.IsActive);

            return cgroup;
        }
    }
}
