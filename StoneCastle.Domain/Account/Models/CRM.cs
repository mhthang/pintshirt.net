using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Account.Models
{
    [Table("CRMs")]
    public class CRM : IEntity<System.Guid>
    {
        public CRM()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Manager Manager { get; set; }

        public bool IsDeleted { get; set; }
    }
}
