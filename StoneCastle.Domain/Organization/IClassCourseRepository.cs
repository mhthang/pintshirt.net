using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Organization
{
    public interface IClassCourseRepository : IRepository<ClassCourse, System.Guid>
    {
        IEnumerable<ClassCourse> GetCoursesByClassRoom(Guid classRoomId);

        List<Models.ClassCourse> SearchCourse(string filter, int pageIndex, int pageSize);
        List<Models.ClassCourse> SearchSemesterCourse(Guid semesterId, string filter, int pageIndex, int pageSize);

        Models.ClassCourse CreateCourse(Guid? classRoomId, Guid? subjectId, Guid? teacherId, Guid? roomId, bool isActive);
        Models.ClassCourse UpdateCourse(Guid id, Guid? classRoomId, Guid? subjectId, Guid? teacherId, Guid? roomId, bool isActive);

        Timetable GetTimetable(Guid courseId);
        Timetable CreateTimetable(Guid courseId, int shiftPerDay, int slotPerShift);

        Guid AddCourse(Guid classRoomId, Guid courseSubjectId);

        int CountSemesterCourse(Guid semesterId);
    }
}
