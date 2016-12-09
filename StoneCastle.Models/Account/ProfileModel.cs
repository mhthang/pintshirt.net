using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Account.Models
{
    public class ProfileModel
    {
        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime DOB { get; set; }

        public string UserId { get; set; }        
        public Application.Models.UserView User { get; set; }

        public string Lang { get; set; }
        public string CountryCode { get; set; }
        public string TimezoneCode { get; set; }

        public string AvatarPhoto { get; set; }
        public string HighlightColor { get; set; }

        public USER_TYPE UserType { get; set; }
        public PROFILE_TYPE ProfileType { get; set; }

        public bool IsDeleted { get; set; }

        public string FullName {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
