using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Scheduler.Models
{
    public class TimetableModel
    {
        public Guid Id { get; set; }

        public int ShiftPerDay { get; set; }

        public int SlotPerShift { get; set; }

        public ICollection<CourseSectionSchedule> CourseSections { get; set; }

        public CourseSectionSchedule[,] TimeTableMatrix { get; set; }

        public CourseSectionSchedule[,,] TimeTableMatrix2 { get; set; }

        public Guid ReferenceObjectId { get; set; }
        public TIMETABLE_TYPE Type { get; set; }
    }
}
