using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.TrainingProgram.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class ClassCourseModel
    {
        public ClassCourseModel()
        {
            //CourseWeeklySchedules = new HashSet<Scheduling.Models.CourseWeeklySchedule>();
        }

        public System.Guid Id { get; set; }

        public System.Guid? TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        public System.Guid? ClassRoomId { get; set; }
        public ClassRoomModel ClassRoom { get; set; }

        public System.Guid? CourseId { get; set; }
        //public CourseSubjectModel CourseSubject { get; set; }

        public System.Guid? RoomId { get; set; }
        public RoomModel Room { get; set; }

        //public virtual IEnumerable<Scheduling.Models.CourseWeeklySchedule> CourseWeeklySchedules { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
