using StoneCastle.Domain;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StoneCastle.Organization.Repositories
{
    public class SubjectRepository : Repository<Organization.Models.Subject, System.Guid>, ISubjectRepository
    {
        public SubjectRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.Subject> SearchSubject(string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().ToList();
        }

        public List<Models.Subject> SearchSemesterSubject(Guid semesterId, Guid? subjectGroupId, string filter, int pageIndex, int pageSize)
        {
            var subjects = GetAll().Where(x => (x.SubjectGroup.SemesterId == semesterId) &&
                ((subjectGroupId == null || subjectGroupId == Guid.Empty) || x.SubjectGroupId == subjectGroupId) &&
                (string.IsNullOrWhiteSpace(filter) || x.Name.ToLower().Contains(filter.Trim().ToLower())))
                .OrderBy(x => x.Name)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return subjects;
        }

        public long CountSemesterSubject(Guid semesterId, Guid? subjectGroupId, string filter)
        {
            var subjects = this.GetAll().Count(x => (x.SubjectGroup.SemesterId == semesterId) &&
            ((subjectGroupId == null || subjectGroupId == Guid.Empty) || x.SubjectGroupId == subjectGroupId) &&
                (string.IsNullOrWhiteSpace(filter) || x.Name.ToLower().Contains(filter.Trim().ToLower())));

            return subjects;
        }

        public Models.Subject CreateSubject(string name, Guid subjectGroupId, string HighlightColor, bool isActive)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (subjectGroupId == null || subjectGroupId == System.Guid.Empty)
                throw new ArgumentNullException("subjectGroupId");

            Models.Subject subject = new Models.Subject()
            {
                Id = Guid.NewGuid(),
                SubjectGroupId = subjectGroupId,
                Name = name,
                HighlightColor = HighlightColor,
                IsActive = isActive,
                IsDeleted = false
            };

            this.DataContext.Insert<Models.Subject>(subject);

            return subject;
        }

        public Models.Subject UpdateSubject(Guid id, string name, Guid subjectGroupId, string HighlightColor, bool isActive)
        {
            Models.Subject subject = this.GetById(id);

            if (subject == null)
                throw new InvalidOperationException($"Subject ({id}) does not exist.");

            subject.Name = name;
            subject.SubjectGroupId = subjectGroupId;
            subject.HighlightColor = HighlightColor;
            subject.IsActive = isActive;

            this.DataContext.Update<Models.Subject, Guid>(subject, x => x.Name, x => x.HighlightColor, x => x.IsActive);

            return subject;
        }

        public int CountSemesterSubject(Guid semesterId)
        {
            List<Guid> groups = this.DataContext.Get<Organization.Models.SubjectGroup>().Where(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted).Select(x => x.Id).ToList();

            return this.GetAll().Count(x => groups.Contains(x.SubjectGroupId) && x.IsActive && !x.IsDeleted);
        }
    }
}

