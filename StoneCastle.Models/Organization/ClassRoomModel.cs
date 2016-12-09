using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Organization.Models
{
    public class ClassRoomModel
    {
        public ClassRoomModel()
        {
            Courses = new HashSet<ClassCourseModel>();
            //ClassEvents = new HashSet<Schedule.Models.ClassEvent>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        [StringLength(32, ErrorMessage = "Code cannot be longer than 32 characters.")]
        public string Code { get; set; }

        public string LogoUrl { get; set; }
        public string HighlightColor { get; set; }

        public System.Guid? ClassGroupId { get; set; }
        public ClassGroupModel ClassGroup { get; set; }

        public System.Guid? TeacherId { get; set; }
        public TeacherModel HomeroomTeacher { get; set; }

        public System.Guid? RoomId { get; set; }
        public virtual RoomModel Room { get; set; }

        public virtual IEnumerable<ClassCourseModel> Courses { get; set; }
        //public virtual IEnumerable<Schedule.Models.ClassEvent> ClassEvents { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
