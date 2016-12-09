using StoneCastle.Domain;
using System;

namespace StoneCastle.Schedule
{
    public interface ICourseSectionRepository : IRepository<Schedule.Models.CourseSection, System.Guid>
    {
        Guid GetTimetableIdFromCourseSection(Guid courseSectionId);
        void DeleteCourseSection(Guid id);
    }
}
