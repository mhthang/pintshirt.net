﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class ClassScheduleBoard
    {
        public ClassScheduleBoard()
        {
            ClassGroups = new HashSet<ClassGroupSchedule>();
        }

        public Guid Id { get; set; }

        public int WorkingDays { get; set; }
        public int ShiftPerDay { get; set; }
        public int SlotPerShift { get; set; }

        public ICollection<ClassGroupSchedule> ClassGroups { get; set; }
    }
}
