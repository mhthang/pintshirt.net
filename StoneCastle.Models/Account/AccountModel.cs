using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Account.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public System.Guid Id { get; set; }

        public System.Guid ProfileId { get; set; }

        public ProfileModel Profile { get; set; }

        public bool IsActive { get; set; }

        public USER_TYPE UserType { get; set; }
        public PROFILE_TYPE ProfileType { get; set; }
    }
}
