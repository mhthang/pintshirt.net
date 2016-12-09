using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using StoneCastle.TrainingProgram.Models;

namespace StoneCastle.Scheduler.Models
{
    public class CourseSectionSchedule
    {
        public CourseSectionSchedule()
        {
            this.Stage = COURSE_SECTION_STAGE.CLOSED;
            this.Shift = SHIFT.MORNING;

            this.Checked = false;
        }

        public System.Guid Id { get; set; }

        public DayOfWeek Day { get; set; }

        public SHIFT Shift { get; set; }

        public short Slot { get; set; }

        public COURSE_SECTION_STAGE Stage { get; set; }

        public System.Guid TimetableId { get; set; }
        public virtual TimetableModel Timetable { get; set; }

        public TeacherScheduleModel Teacher { get; set; }
        public ClassCourseSchedule ClassCourse { get; set; }
        public Course Course { get; set; }

        public bool Checked { get; set; }

    }
}
