using StoneCastle.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Application.Models
{
    [Table("AppFunctions")]
    public class AppFunction : IEntity<Guid>
    {
        public AppFunction()
        {
            this.AppClaims = new HashSet<AppClaim>();
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(256, ErrorMessage = "Name cannot be longer than 256 characters.")]
        public string Name { get; set; }

        public virtual ICollection<AppClaim> AppClaims { get; set; }

        public bool IsDeleted { get; set; }
    }
}
