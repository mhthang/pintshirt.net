using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Scheduler.Models
{
    public class TeacherModel
    {
        public TeacherModel()
        {
            WeekSchedule = new HashSet<DaySchedule>();
        }

        public System.Guid Id { get; set; }

        public System.Guid AccountId { get; set; }
        public Account.Models.AccountModel Account { get; set; }

        public System.Guid? DivisionId { get; set; }

        public bool IsActive { get; set; }

        public TimetableModel Timetable { get; set; }   
        
        public ICollection<DaySchedule> WeekSchedule { get; set; }
    }
}
