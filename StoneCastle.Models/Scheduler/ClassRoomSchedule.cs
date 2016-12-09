using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;

namespace StoneCastle.Scheduler.Models
{
    public class ClassRoomSchedule
    {
        public ClassRoomSchedule()
        {
            Courses = new List<ClassCourseSchedule>();
            //ClassTimetables = new List<ClassTimetableModel>();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }

        public string HighlightColor { get; set; }
        public string LogoUrl { get; set; }

        public System.Guid? ClassGroupId { get; set; }

        //public ClassGroupSchedule ClassGroup { get; set; }

        public TimetableModel Timetable { get; set; }

        public System.Guid? TeacherId { get; set; }

        public ICollection<ClassCourseSchedule> Courses { get; set; }
        //public ICollection<ClassTimetableModel> ClassTimetables { get; set; }
    }
}
