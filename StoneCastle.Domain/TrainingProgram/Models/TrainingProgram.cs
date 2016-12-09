using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.TrainingProgram.Models
{
    [Table("TrainingPrograms")]
    public class TrainingProgram : IEntity<System.Guid>
    {
        public TrainingProgram()
        {
            CourseSubjects = new HashSet<Course>();
            ClassGroups = new HashSet<ClassGroup>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        public System.Guid SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }
        [StringLength(255, ErrorMessage = "Url cannot be longer than 255 characters.")]
        public string LogoUrl { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Course> CourseSubjects { get; set; }

        public virtual IEnumerable<ClassGroup> ClassGroups { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public bool IsDeleted { get; set; }
    }
}
