using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneCastle.Schedule.Models
{
    public class ClassTimetableModel
    {
        public ClassTimetableModel()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid ClassRoomId { get; set; }
        public ClassRoomSchedule ClassRoom { get; set; }

        public System.Guid TimetableId { get; set; }
        public TimetableModel Timetable { get; set; }

        public System.Guid SchedulingTableId { get; set; }
        public SchedulingTableModel SchedulingTable { get; set; }

    }
}
