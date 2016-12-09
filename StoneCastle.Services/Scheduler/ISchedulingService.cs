using StoneCastle.Schedule.Models;
using StoneCastle.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services.Scheduler
{
    public interface ISchedulingService : IBaseService
    {
        ScheduleBoard ProceedGeneratingScheduleBoard(Guid scheduleId);

        Schedule.Models.ScheduleStageInfo ValidateScheduleBoard(Guid scheduleId);
        ScheduleBoard GenerateScheduleBoard(Guid scheduleId);
        Schedule.Models.ScheduleStageInfo AdjustScheduleBoard(Guid scheduleId);
        Schedule.Models.ScheduleStageInfo CompleteScheduleBoard(Guid scheduleId);

        Schedule.Models.ScheduleStageInfo CalculateSemesterScheduleBoard(Guid semesterId);

        void ClearScheduleBoard(Guid scheduleId);

        ScheduleBoard LoadScheduleBoard(Guid scheduleId);
        void SaveScheduleBoard(ScheduleBoard scheduleBoard);
        TeacherSchedule LoadTeacherScheduleBoard(Guid scheduleId);
        ClassScheduleBoard LoadClassScheduleBoard(Guid scheduleId);

        void SaveScheduleBoard(UpdateTimetableBoardModel model);

    }
}
