using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("Subjects")]
    public class Subject : IEntity<System.Guid>
    {
        public Subject()
        {
            Teachers = new HashSet<Teacher>();
            CourseSubjects = new HashSet<TrainingProgram.Models.Course>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public System.Guid SubjectGroupId { get; set; }
        [ForeignKey("SubjectGroupId")]
        public virtual SubjectGroup SubjectGroup { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual IEnumerable<TrainingProgram.Models.Course> CourseSubjects { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
