using StoneCastle.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Account.Models
{
    public class TeacherViewModel
    {
        public TeacherViewModel()
        {
        }

        public System.Guid Id { get; set; }
        public System.Guid AccountId { get; set; }
        public System.Guid ProfileId { get; set; }

        public System.Guid DivisionId { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        public String HighlightColor { get; set; }
        
        public Boolean IsActive { get; set; }

        public String FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }

    }
}
