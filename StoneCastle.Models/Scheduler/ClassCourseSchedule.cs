using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class ClassCourseSchedule
    {
        public ClassCourseSchedule()
        {
            
        }

        public System.Guid Id { get; set; }

        public System.Guid? TeacherId { get; set; }
        public TeacherScheduleModel Teacher { get; set; }

        public System.Guid? CourseId { get; set; }
        public CourseSchedule Course { get; set; }

        public System.Guid? ClassRoomId { get; set; }
        public ClassRoomSchedule ClassRoom { get; set; }

        public System.Guid? TimetableId { get; set; }
        public TimetableModel Timetable { get; set; }        
    }
}
