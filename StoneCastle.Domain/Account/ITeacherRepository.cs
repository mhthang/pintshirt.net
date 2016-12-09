using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Account
{
    public interface ITeacherRepository : IRepository<Account.Models.Teacher, System.Guid>
    {
        List<Models.Teacher> SearchTeacher(string filter, int pageIndex, int pageSize);
        List<Models.Teacher> SearchSemesterTeacher(Guid semesterId, string filter, int pageIndex, int pageSize);
        List<Models.Teacher> GetSemesterTeacher(Guid semesterId);
        List<Models.Teacher> GetAvailableSemesterHomeroomTeachers(Guid semesterId);

        Models.Teacher CreateTeacher(Guid divisionId, string firstName, string lastName, string email, string phone, string highlightColor, bool isActive);
        Models.Teacher UpdateTeacher(Guid id, Guid divisionId, string firstName, string lastName, string email, string phone, string highlightColor, bool isActive);

        Timetable GetTeacherTimetable(Guid teacherId, Guid scheduleId);

        int CountSemesterTeacher(Guid semesterId);
    }
}
