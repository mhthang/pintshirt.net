using StoneCastle.Organization.Models;
using StoneCastle.TrainingProgram.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace StoneCastle.Schedule.Models
{
    public class CourseSectionModel
    {
        [Key]
        public System.Guid Id { get; set; }

        public DayOfWeek Day { get; set; }

        public SHIFT Shift { get; set; }

        public short Slot { get; set; }

        public COURSE_SECTION_STAGE Stage { get; set; }

        public System.Guid TimetableId { get; set; }

        public System.Guid? ClassCourseId { get; set; }
        public ClassCourseModel ClassCourse { get; set; }

    }
}
