using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Services;
using StoneCastle.TrainingProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.TrainingProgram.Services
{
    public interface ITrainingProgramService : IBaseService
    {
        SearchResponse<TrainingProgramModel> SearchTrainingProgram(SearchRequest request);
        SearchResponse<TrainingProgramModel> SearchSemesterTrainingProgram(SearchRequest request);
        List<TrainingProgramModel> GetSemesterPrograms(Guid semesterId);

        TrainingProgramModel GetTrainingProgram(TrainingProgramModel model);
        TrainingProgramModel CreateOrUpdate(TrainingProgramModel model);
    }
}
