using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System;

namespace StoneCastle.Schedule
{
    public interface IClassTimetableRepository : IRepository<ClassTimetable, System.Guid>
    {
        ClassTimetable Create(Guid scheduleId, Guid classRoomId, Timetable timetable);
        Timetable GetTimetable(Guid scheduleId, Guid classRoomId);
    }
}
