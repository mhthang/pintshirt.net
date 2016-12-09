using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Scheduler.Models
{
    public class TimeShift
    {
        public TimeShift()
        {

        }

        public String Caption { get; set; }
        public DayOfWeek Day { get; set; }
        public int Shift { get; set; }
        public int Slot { get; set; }
    };
}
