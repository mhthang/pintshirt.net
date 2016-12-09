using System;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    [Table("ScheduleEvents")]
    public class ScheduleEvent : IEntity<System.Guid>
    {
        public ScheduleEvent()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public SCHEDULE_STAGE Stage { get; set; }

        public System.Guid SchedulingTableId { get; set; }
        [ForeignKey("SchedulingTableId")]
        public virtual SchedulingTable SchedulingTable { get; set; }

        public virtual IEnumerable<ClassGroupEvent> ClassGroupEvents { get; set; }

    }
}
