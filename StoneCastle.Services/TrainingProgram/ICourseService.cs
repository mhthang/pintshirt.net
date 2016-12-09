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
    public interface ICourseService : IBaseService
    {
        SearchResponse<CourseModel> SearchCourse(SearchRequest request);
        CourseModel GetCourse(CourseModel model);
        CourseModel CreateOrUpdate(CourseModel model);
    }
}
