using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class ScheduleBoard
    {
        public ScheduleBoard()
        {
            this.ClassGroups = new List<ClassGroupSchedule>();
        }

        public Guid Id { get; set; }

        public List<ClassGroupSchedule> ClassGroups { get; set; }        
    }
}
