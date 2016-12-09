using System;
using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("CourseSections")]
    public class CourseSection : IEntity<System.Guid>
    {
        public CourseSection()
        {
            this.Stage = COURSE_SECTION_STAGE.OPEN;
            this.Shift = SHIFT.MORNING;
        }

        [Key]
        public System.Guid Id { get; set; }

        public DayOfWeek Day { get; set; }

        public SHIFT Shift { get; set; }

        public short Slot { get; set; }

        public COURSE_SECTION_STAGE Stage { get; set; }

        public System.Guid TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public System.Guid? ClassCourseId { get; set; }
        [ForeignKey("ClassCourseId")]
        public virtual Organization.Models.ClassCourse ClassCourse { get; set; }

        //public System.Guid? TeacherId { get; set; }
        //[ForeignKey("TeacherId")]
        //public virtual Teacher Teacher { get; set; }

        //public System.Guid? CourseSubjectId { get; set; }
        //[ForeignKey("CourseSubjectId")]
        //public virtual TrainingProgram.Models.CourseSubject CourseSubject { get; set; }

    }
}
