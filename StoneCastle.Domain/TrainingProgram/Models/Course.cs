using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.TrainingProgram.Models
{
    [Table("Courses")]
    public class Course : IEntity<System.Guid>
    {
        public Course()
        {
            Courses = new HashSet<ClassCourse>();
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid TrainingProgramId { get; set; }
        [ForeignKey("TrainingProgramId")]
        public virtual TrainingProgram TrainingProgram { get; set; }

        public System.Guid? SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        //public virtual IEnumerable<Client> Clients { get; set; }
        public virtual IEnumerable<ClassCourse> Courses { get; set; }

        public int TotalSection { get; set; }
        public int SectionPerWeek { get; set; }
        public bool IsTeachingByHomeroomTeacher { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
