using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class BuildingModel
    {
        public BuildingModel()
        {
        }

        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        public System.Guid SemesterId { get; set; }
        public SemesterModel Semester { get; set; }

        public string HighlightColor { get; set; }
        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
