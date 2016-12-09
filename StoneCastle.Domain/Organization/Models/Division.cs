using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("Divisions")]
    public class Division : IEntity<System.Guid>
    {
        public Division()
        {
            TeacherDivisions = new HashSet<TeacherDivision>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }
        [StringLength(255, ErrorMessage = "Url cannot be longer than 255 characters.")]
        public string LogoUrl { get; set; }

        public System.Guid SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        public virtual ICollection<TeacherDivision> TeacherDivisions { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
