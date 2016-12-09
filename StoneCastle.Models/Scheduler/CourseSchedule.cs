using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class CourseSchedule
    {
        public CourseSchedule()
        {
        }

        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string HighlightColor { get; set; }

        public int SectionPerWeek { get; set; }

        public TrainingProgramSchedule TrainingProgram { get; set; }

        public TimetableModel Timetable { get; set; }       
    }
}
