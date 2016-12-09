using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using StoneCastle.TrainingProgram.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.TrainingProgram
{
    public interface ICourseRepository : IRepository<Course, System.Guid>
    {
        IEnumerable<Course> GetCourseSubjectByTrainingProgram(Guid programId);

        List<Models.Course> SearchCourseSubject(string filter, int pageIndex, int pageSize);
        List<Models.Course> SearchSemesterCourseSubject(Guid semesterId, string filter, int pageIndex, int pageSize, ref int total);

        Models.Course CreateCourseSubject(Guid programId, Guid subjectId, string name, string shortName, int totalSections, int sectionPerWeek, bool isTeachingByHomeroomTeacher, bool isActive);
        Models.Course UpdateCourseSubject(Guid id, Guid subjectId, string name, string shortName, int totalSections, int sectionPerWeek, bool isTeachingByHomeroomTeacher, bool isActive);

        Timetable GetTimetable(Guid courseSubjectId);
        Timetable CreateTimetable(Guid courseSubjectId, int shiftPerDay, int slotPerShift);
    }
}
