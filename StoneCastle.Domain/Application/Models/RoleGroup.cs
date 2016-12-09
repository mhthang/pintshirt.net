using StoneCastle.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Application.Models
{
    [Table("RoleGroups")]
    public class RoleGroup : IEntity<Guid>
    {
        public RoleGroup()
        {
            Users = new HashSet<User>();
            GroupClaims = new HashSet<GroupClaim>();
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(256, ErrorMessage = "Name cannot be longer than 256 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<GroupClaim> GroupClaims { get; set; }

        public bool IsDeleted { get; set; }
    }
}
