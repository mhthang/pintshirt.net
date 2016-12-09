using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Account.Services
{
    public interface ITeacherService : IBaseService
    {
        SearchTeacherResponse SearchTeachers(SearchRequest request);
        TeacherViewModel GetTeacher(TeacherModel model);
        IEnumerable<TeacherViewModel> GetSemesterTeachers(SearchRequest request);
        IEnumerable<TeacherViewModel> GetAvailableSemesterHomeroomTeachers(SearchRequest request);
        TeacherModel CreateOrUpdate(TeacherViewModel model);
    }
}
