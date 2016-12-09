using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using StoneCastle.TrainingProgram.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    [Table("ClassCourses")]
    public class ClassCourse : IEntity<System.Guid>
    {
        public ClassCourse()
        {
            //CourseWeeklySchedules = new HashSet<Scheduling.Models.CourseWeeklySchedule>();
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        public System.Guid? ClassRoomId { get; set; }
        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }

        public System.Guid? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public System.Guid? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        //public virtual IEnumerable<Schedule.Models.CourseWeeklySchedule> CourseWeeklySchedules { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
