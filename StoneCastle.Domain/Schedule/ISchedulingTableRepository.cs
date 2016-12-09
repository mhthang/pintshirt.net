using StoneCastle.Domain;
using System;
using System.Collections.Generic;

namespace StoneCastle.Schedule
{
    public interface ISchedulingTableRepository : IRepository<Schedule.Models.SchedulingTable, System.Guid>
    {
        List<Models.SchedulingTable> SearchSemesterSchedule(Guid semesterId, string filter, int pageIndex, int pageSize);

        Models.SchedulingTable CreateSchedule(Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        Models.SchedulingTable UpdateSchedule(Guid id, Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);

        Models.SchedulingTable CreateSchedule(Guid semesterId);

        void ChangedValidated(Guid scheduleId);
        void ChangeGeneratingStarted(Guid scheduleId);
        void ChangeGeneratingCompleted(Guid scheduleId);
        void ChangeAdjust(Guid scheduleId);
        void ChangeCompleted(Guid scheduleId);

        void Reset(Guid scheduleId);
        Models.SchedulingTable GetSemesterDefaultSchedule(Guid semesterId);
    }
}
