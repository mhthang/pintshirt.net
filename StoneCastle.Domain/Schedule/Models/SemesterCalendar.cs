using StoneCastle.Account.Models;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("SemesterCalendars")]
    public class SemesterCalendar : IEntity<System.Guid>
    {
        public SemesterCalendar()
        {
            SemesterEvents = new HashSet<SemesterEvent>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public virtual IEnumerable<SemesterEvent> SemesterEvents { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
