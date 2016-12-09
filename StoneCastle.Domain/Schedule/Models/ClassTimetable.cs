using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("ClassTimetables")]
    public class ClassTimetable : IEntity<System.Guid>
    {
        public ClassTimetable()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid ClassRoomId { get; set; }
        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }

        public System.Guid TimetableId { get; set; }
        [ForeignKey("TimetableId")]
        public virtual Timetable Timetable { get; set; }

        public System.Guid SchedulingTableId { get; set; }
        [ForeignKey("SchedulingTableId")]
        public virtual SchedulingTable SchedulingTable { get; set; }

    }
}
