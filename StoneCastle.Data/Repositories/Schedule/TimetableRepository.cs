using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Schedule.Repositories
{
    public class TimetableRepository : Repository<Schedule.Models.Timetable, System.Guid>, ITimetableRepository
    {
        public TimetableRepository(ISCDataContext context) : base(context)
        {
        }
    }
}
