using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("Buildings")]
    public class Building : IEntity<System.Guid>
    {
        public Building()
        {
            Rooms = new HashSet<Room>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        public System.Guid SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        [StringLength(255, ErrorMessage = "Url cannot be longer than 255 characters.")]
        public string LogoUrl { get; set; }

        public virtual IEnumerable<Room> Rooms { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
