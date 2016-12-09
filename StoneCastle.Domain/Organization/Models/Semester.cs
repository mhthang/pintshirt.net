using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("Semesters")]
    public class Semester : IEntity<System.Guid>
    {
        public Semester()
        {
            Devisions = new HashSet<Division>();
            ClassGroups = new HashSet<ClassGroup>();
            SemesterCalendars = new HashSet<Schedule.Models.SemesterCalendar>();
            Buildings = new HashSet<Building>();
            SubjectGroups = new HashSet<SubjectGroup>();
            TrainingPrograms = new HashSet<TrainingProgram.Models.TrainingProgram>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "ShortName cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        public System.Guid OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public virtual IEnumerable<Division> Devisions { get; set; }
        public virtual IEnumerable<ClassGroup> ClassGroups { get; set; }
        public virtual IEnumerable<Schedule.Models.SemesterCalendar> SemesterCalendars { get; set; }
        public virtual IEnumerable<Building> Buildings { get; set; }
        public virtual IEnumerable<SubjectGroup> SubjectGroups { get; set; }
        public virtual IEnumerable<TrainingProgram.Models.TrainingProgram> TrainingPrograms { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
