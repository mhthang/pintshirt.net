using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class TrainingProgramSchedule
    {
        public TrainingProgramSchedule()
        {
            //ClassGroups = new List<ClassGroupSchedule>();
            CourseSubjects = new List<CourseSchedule>();
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string LogoUrl { get; set; }

        //public ICollection<ClassGroupSchedule> ClassGroups { get; set; }

        public ICollection<CourseSchedule> CourseSubjects { get; set; }

        public TimetableModel Timetable { get; set; }
    }
}
