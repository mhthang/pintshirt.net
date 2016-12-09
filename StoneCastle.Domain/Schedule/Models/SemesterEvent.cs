using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("SemesterEvents")]
    public class SemesterEvent : IEntity<System.Guid>
    {
        public SemesterEvent()
        {
            ClassGroupEvents = new HashSet<ClassGroupEvent>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public System.Guid SemesterCalendarId { get; set; }
        [ForeignKey("SemesterCalendarId")]
        public virtual SemesterCalendar SemesterCalendar { get; set; }

        //public System.Guid? WeeklyTimeTableId { get; set; }
        //[ForeignKey("WeeklyTimeTableId")]
        //public virtual WeeklyTimeTable WeeklyTimeTable { get; set; }

        public virtual IEnumerable<ClassGroupEvent> ClassGroupEvents { get; set; }

        public bool IsDeleted { get; set; }
    }
}
