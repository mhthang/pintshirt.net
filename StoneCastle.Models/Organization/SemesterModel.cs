using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class SemesterModel
    {
        public SemesterModel()
        {
            Devisions = new HashSet<DivisionModel>();
            ClassGroups = new HashSet<ClassGroupModel>();
            //SemesterCalendars = new HashSet<Schedule.Models.SemesterCalendar>();
            Buildings = new HashSet<BuildingModel>();
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

        public IEnumerable<DivisionModel> Devisions { get; set; }
        public IEnumerable<ClassGroupModel> ClassGroups { get; set; }
        //public IEnumerable<Schedule.Models.SemesterCalendar> SemesterCalendars { get; set; }
        public IEnumerable<BuildingModel> Buildings { get; set; }

        public bool IsDeleted { get; set; }
    }
}
