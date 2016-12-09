using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Schedule.Repositories
{
    public class CourseSectionRepository : Repository<Schedule.Models.CourseSection, System.Guid>, ICourseSectionRepository
    {
        public CourseSectionRepository(ISCDataContext context) : base(context)
        {
        }

        public void DeleteCourseSection(Guid id)
        {
            if (id == null || id == Guid.Empty)
                throw new ArgumentNullException("id");

            Models.CourseSection cs = this.GetById(id);

            if (cs == null)
                throw new InvalidOperationException($"Course Section ({id}) does not exist.");

            this.Delete(cs);
        }

        public Guid GetTimetableIdFromCourseSection(Guid courseSectionId)
        {
            return this.GetAll().Where(x => x.Id == courseSectionId).Select(x => x.TimetableId).FirstOrDefault();
        }
    }
}
