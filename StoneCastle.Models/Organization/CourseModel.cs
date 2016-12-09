using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.TrainingProgram.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
            //CourseWeeklySchedules = new HashSet<Scheduling.Models.CourseWeeklySchedule>();
        }

        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Short Name cannot be longer than 500 characters.")]
        public string ShortName { get; set; }

        public System.Guid? TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        public System.Guid? ClassRoomId { get; set; }
        public ClassRoomModel ClassRoom { get; set; }

        public System.Guid? CourseSubjectId { get; set; }
        //public CourseSubjectModel CourseSubject { get; set; }

        public System.Guid? LearningRoomId { get; set; }
        public LearningRoomModel LearningRoom { get; set; }

        //public virtual IEnumerable<Scheduling.Models.CourseWeeklySchedule> CourseWeeklySchedules { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
