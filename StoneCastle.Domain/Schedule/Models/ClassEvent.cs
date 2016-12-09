using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("ClassEvents")]
    public class ClassEvent : IEntity<System.Guid>
    {
        public ClassEvent()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }

        public System.Guid ClassRoomId { get; set; }
        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }

        public System.Guid ClassGroupEventId { get; set; }
        [ForeignKey("ClassGroupEventId")]
        public virtual ClassGroupEvent ClassGroupEvent { get; set; }

        public System.Guid? TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }


        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
