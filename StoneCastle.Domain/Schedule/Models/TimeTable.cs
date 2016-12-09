using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("Timetables")]
    public class Timetable : IEntity<System.Guid>
    {
        public Timetable()
        {
            this.CourseSections = new HashSet<CourseSection>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        public int ShiftPerDay { get; set; }
        public int SlotPerShift { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [StringLength(32, ErrorMessage = "HighlightColor cannot be longer than 32 characters.")]
        public string HighlightColor { get; set; }
        public string Note { get; set; }

        public virtual ICollection<CourseSection> CourseSections { get; set; }
    }
}
