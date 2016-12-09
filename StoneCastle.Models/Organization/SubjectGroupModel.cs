using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class SubjectGroupModel
    {
        public SubjectGroupModel()
        {
            Subjects = new HashSet<SubjectModel>();
        }

        public System.Guid Id { get; set; }

        public System.Guid SemesterId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        
        public string HighlightColor { get; set; }

        public ICollection<SubjectModel> Subjects { get; set; }

        public bool IsActive { get; set; }

    }
}
