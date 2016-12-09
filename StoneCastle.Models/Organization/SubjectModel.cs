using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class SubjectModel
    {
        public SubjectModel()
        {
        }

        public System.Guid Id { get; set; }
        public System.Guid SubjectGroupId { get; set; }
        public SubjectGroupModel SubjectGroup { get; set; }

        public string Name { get; set; }        
        public string HighlightColor { get; set; }

        public bool IsActive { get; set; }

    }
}
