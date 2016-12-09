using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("Organizations")]
    public class Organization : IEntity<System.Guid>
    {
        public Organization()
        {
            Clients = new HashSet<Client>();
            Buildings = new HashSet<Building>();
            Semesters = new HashSet<Semester>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Short Name cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        [StringLength(255, ErrorMessage = "Url cannot be longer than 255 characters.")]
        public string LogoUrl { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<Client> Clients { get; set; }
        public virtual IEnumerable<Building> Buildings { get; set; }
        public virtual ICollection<Semester> Semesters { get; set; }

        public bool IsDeleted { get; set; }
    }
}
