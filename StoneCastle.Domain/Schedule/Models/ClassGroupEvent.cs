using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("ClassGroupEvents")]
    public class ClassGroupEvent : IEntity<System.Guid>
    {
        public ClassGroupEvent()
        {
            ClassEvents = new HashSet<ClassEvent>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public System.Guid SemesterEventId { get; set; }
        [ForeignKey("SemesterEventId")]
        public virtual SemesterEvent SemesterEvent { get; set; }

        public System.Guid ClassGroupId { get; set; }
        [ForeignKey("ClassGroupId")]
        public virtual ClassGroup ClassGroup { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public virtual IEnumerable<ClassEvent> ClassEvents { get; set; }

        public bool IsDeleted { get; set; }
    }
}
