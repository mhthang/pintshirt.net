using System;
using StoneCastle.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    public class ScheduleEventModel
    {
        public ScheduleEventModel()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public SCHEDULE_STAGE Stage { get; set; }

        public System.Guid SchedulingTableId { get; set; }
        public SchedulingTableModel SchedulingTable { get; set; }

        //public ICollection<ClassGroupEventModel> ClassGroupEvents { get; set; }

    }
}
