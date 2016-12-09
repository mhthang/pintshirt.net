using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Organization.Services
{
    public interface IClassCourseService : IBaseService
    {
        SearchResponse<ClassCourseModel> SearchClassCourse(SearchRequest request);
        SearchResponse<CourseListModel> SearchSemesterClassCourse(SearchRequest request);

        ClassCourseModel GetClassCourse(ClassCourseModel model);
        IEnumerable<ClassCourseModel> GetClassCoursesByClassRoomId(System.Guid id);
        ClassCourseModel CreateOrUpdate(ClassCourseModel model);

        int GenerateSemesterClassCourse(System.Guid id);
    }
}
