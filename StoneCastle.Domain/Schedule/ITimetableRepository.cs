using StoneCastle.Domain;

namespace StoneCastle.Schedule
{
    public interface ITimetableRepository : IRepository<Schedule.Models.Timetable, System.Guid>
    {
    }
}
