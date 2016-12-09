using StoneCastle.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services.Scheduler
{
    public interface ITimetableService : IBaseService
    {
        TimetableModel GetTimetable(Guid timetableId);
        TimetableModel GetTimetable(TimetableModel model);

        TimetableModel GetSemesterTimetable(Guid semesterId);
        TimetableModel GetProgramTimetable(Guid programId);
        TimetableModel GetSubjectTimetable(Guid subjectId);
        TimetableModel GetCourseTimetable(Guid courseId);
        TimetableModel GetTeacherTimetable(Guid teacherId);
        TimetableModel GetClassTimetable(Guid classRoomId);

        TimetableModel GetTimeTable(int shifts);
        TimetableModel GetWorkingTimeTable(int shifts, int slotPerShift);

        Guid SaveTimetable(TimetableModel timetable);
    }
}
