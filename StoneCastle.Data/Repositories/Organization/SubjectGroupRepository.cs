using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StoneCastle.Organization.Repositories
{
    public class SubjectGroupRepository : Repository<Organization.Models.SubjectGroup, System.Guid>, ISubjectGroupRepository
    {
        public SubjectGroupRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.SubjectGroup> SearchSubjectGroup(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.SubjectGroup> SearchSemesterSubjectGroup(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x => x.SemesterId == semesterId && !x.IsDeleted).ToList();
        }

        public List<Models.SubjectGroup> GetSemesterSubjectGroup(Guid semesterId)
        {
            return this.GetAll().Where(x=>x.SemesterId == semesterId && !x.IsDeleted).ToList();
        }

        public Models.SubjectGroup CreateSubjectGroup(Guid semesterId, string name, string shortName, string highlightColor, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Models.SubjectGroup subjectGroup = new Models.SubjectGroup()
            {
                Id = Guid.NewGuid(),
                SemesterId = semesterId,
                Name = name,
                Code = shortName,
                HighlightColor = highlightColor,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.SubjectGroup>(subjectGroup);

            return subjectGroup;
        }

        public Models.SubjectGroup UpdateSubjectGroup(Guid id, string name, string shortName, string highlightColor, bool isActive)
        {
            Models.SubjectGroup subjectGroup = this.GetById(id);

            if (subjectGroup == null)
                throw new InvalidOperationException($"SubjectGroup ({id}) does not exist.");

            subjectGroup.Name = name;
            subjectGroup.Code = shortName;
            subjectGroup.HighlightColor = highlightColor;
            subjectGroup.IsActive = isActive;

            this.DataContext.Update<Models.SubjectGroup, Guid>(subjectGroup, x => x.Name, x => x.HighlightColor, x => x.IsActive);

            return subjectGroup;
        }

        public int CountSemesterSubjectGroup(Guid semesterId)
        {
            return this.GetAll().Count(x =>x.SemesterId == semesterId && x.IsActive && !x.IsDeleted);
        }
    }
}
