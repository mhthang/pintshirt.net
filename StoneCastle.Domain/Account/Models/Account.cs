using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Account.Models
{
    [Table("Accounts")]
    public class Account : IEntity<System.Guid>
    {
        public Account()
        {
            Managers = new HashSet<Manager>();
            CRMs = new HashSet<CRM>();
        }

        public System.Guid Id { get; set; }

        public System.Guid ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public bool IsActive { get; set; }

        public USER_TYPE UserType { get; set; }
        public PROFILE_TYPE ProfileType { get; set; }

        public virtual IEnumerable<Manager> Managers { get; set; }
        public virtual IEnumerable<CRM> CRMs { get; set; }

        public bool IsDeleted { get; set; }
    }
}
