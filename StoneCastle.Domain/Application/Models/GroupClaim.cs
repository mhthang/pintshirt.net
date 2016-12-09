using StoneCastle.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Application.Models
{
    [Table("GroupClaims")]
    public class GroupClaim : IEntity<Guid>
    {
        public GroupClaim()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(256, ErrorMessage = "Type cannot be longer than 256 characters.")]
        public string ClaimType { get; set; }

        [StringLength(500, ErrorMessage = "Value cannot be longer than 500 characters.")]
        public string ClaimValue { get; set; }

        public Guid RoleGroupId { get; set; }

        [ForeignKey("RoleGroupId")]
        public virtual RoleGroup RoleGroup { get; set; }

        public bool IsDeleted { get; set; }
    }
}
