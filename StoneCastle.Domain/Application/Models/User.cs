//using Microsoft.AspNet.Identity.EntityFramework;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Application.Models
{
    [Table("Users")]
    public class User :  IEntity<string>
    {
        public User()
        {
            RoleGroups = new HashSet<RoleGroup>();
            //UserClaims = new HashSet<IdentityUserClaim>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Your email looks incorrect. Please check and try again.")]
        [MinLength(8)]
        //[Index("UserNameIndex", IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }


        [MaxLength(100)]
        public string LastName { get; set; }

        public bool ChangePassword { get; set; }

        public bool IsActived { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<RoleGroup> RoleGroups { get; set; }
        //public virtual ICollection<IdentityUserClaim> UserClaims { get; set; }
    }
}
