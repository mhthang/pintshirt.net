using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("Rooms")]
    public class Room : IEntity<System.Guid>
    {
        public Room()
        {
            Courses = new HashSet<ClassCourse>();
            ClassRooms = new HashSet<ClassRoom>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        [StringLength(255, ErrorMessage = "Url cannot be longer than 32 characters.")]
        public string LogoUrl { get; set; }
        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public System.Guid BuildingId { get; set; }
        [ForeignKey("BuildingId")]
        public virtual Building Building { get; set; }

        public virtual IEnumerable<ClassCourse> Courses { get; set; }
        public virtual IEnumerable<ClassRoom> ClassRooms { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
