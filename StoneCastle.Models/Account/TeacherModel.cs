using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Account.Models
{
    public class TeacherModel
    {
        public TeacherModel()
        {
        }

        public System.Guid Id { get; set; }

        public System.Guid AccountId { get; set; }
        public AccountModel Account { get; set; }
    }
}
