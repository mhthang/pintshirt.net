using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("ClassGroups")]
    public class ClassGroup : IEntity<System.Guid>
    {
        public ClassGroup()
        {
            ClassRooms = new HashSet<ClassRoom>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        [StringLength(255, ErrorMessage = "Url cannot be longer than 255 characters.")]
        public string LogoUrl { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public bool IsActive { get; set; }

        public System.Guid SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        public System.Guid? TrainingProgramId { get; set; }
        [ForeignKey("TrainingProgramId")]
        public virtual TrainingProgram.Models.TrainingProgram TrainingProgram { get; set; }

        public virtual ICollection<ClassRoom> ClassRooms { get; set; }       

        public bool IsDeleted { get; set; }
    }
}
