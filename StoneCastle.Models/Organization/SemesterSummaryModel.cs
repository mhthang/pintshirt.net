using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class SemesterSummaryModel
    {
        public SemesterSummaryModel()
        {
        }

        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "ShortName cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        public System.Guid OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }

        public string HighlightColor { get; set; }
        public bool IsActive { get; set; }

        public int TotalProgram { get; set; }
        public int TotalBuilding { get; set; }
        public int TotalRoom { get; set; }
        public int TotalClassRoom { get; set; }
        public int TotalDivision { get; set; }
        public int TotalTeacher { get; set; }
        public int TotalSubjectGroup { get; set; }
        public int TotalSubject { get; set; }
        public int TotalCourse { get; set; }
    }
}
