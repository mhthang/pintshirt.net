using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class UserOrganizationModel
    {
        public UserOrganizationModel()
        {
            Semesters = new HashSet<SemesterModel>();
        }

        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Short Name cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        public string LogoUrl { get; set; }
        public string HighlightColor { get; set; }

        public bool IsActive { get; set; }

        public ICollection<SemesterModel> Semesters { get; set; }
    }
}
