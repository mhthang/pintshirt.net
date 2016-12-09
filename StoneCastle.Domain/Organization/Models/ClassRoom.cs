using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("ClassRooms")]
    public class ClassRoom : IEntity<System.Guid>
    {
        public ClassRoom()
        {
            Courses = new HashSet<ClassCourse>();
            ClassEvents = new HashSet<Schedule.Models.ClassEvent>();

            ClassTimetables = new HashSet<Schedule.Models.ClassTimetable>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }


        [StringLength(255, ErrorMessage = "Url cannot be longer than 32 characters.")]
        public string LogoUrl { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }

        public System.Guid? ClassGroupId { get; set; }
        [ForeignKey("ClassGroupId")]
        public virtual ClassGroup ClassGroup { get; set; }

        public System.Guid? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher HomeroomTeacher { get; set; }

        public System.Guid? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public virtual ICollection<ClassCourse> Courses { get; set; }
        public virtual IEnumerable<Schedule.Models.ClassEvent> ClassEvents { get; set; }

        public virtual IEnumerable<Schedule.Models.ClassTimetable> ClassTimetables { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
