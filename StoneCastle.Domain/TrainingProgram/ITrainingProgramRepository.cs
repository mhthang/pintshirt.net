using StoneCastle.Domain;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.TrainingProgram
{
    public interface ITrainingProgramRepository : IRepository<TrainingProgram.Models.TrainingProgram, System.Guid>
    {
        List<Models.TrainingProgram> SearchTrainingProgram(string filter, int pageIndex, int pageSize);
        List<Models.TrainingProgram> SearchSemesterTrainingProgram(Guid semesterId, string filter, int pageIndex, int pageSize);
        List<Models.TrainingProgram> GetSemesterTrainingProgram(Guid semesterId);

        Models.TrainingProgram CreateTrainingProgram(Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);
        Models.TrainingProgram UpdateTrainingProgram(Guid id, string name, string shortName, string HighlightColor, string logoUrl, bool isActive);

        Timetable GetTimetable(Guid programId);
        Timetable CreateTimetable(Guid programId, int shiftPerDay, int slotPerShift);

        int CountSemesterProgram(Guid semesterId);
    }
}
