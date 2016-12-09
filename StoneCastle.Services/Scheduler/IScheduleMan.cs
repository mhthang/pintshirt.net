using StoneCastle.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services.Scheduler
{
    public interface IScheduleMan : IBaseService
    {
        ScheduleBoard Processing(Guid semesterId);
        TimetableModel GetClassTimetable();
    }
}
