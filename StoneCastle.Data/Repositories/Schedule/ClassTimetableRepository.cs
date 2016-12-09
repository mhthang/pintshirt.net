using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using StoneCastle.Schedule.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Organization.Models;

namespace StoneCastle.Schedule.Repositories
{
    public class ClassTimetableRepository : Repository<ClassTimetable, System.Guid>, IClassTimetableRepository
    {
        public ClassTimetableRepository(ISCDataContext context) : base(context)
        {
        }

        public ClassTimetable Create(Guid scheduleId, Guid classRoomId, Timetable timetable)
        {
            //ClassRoom classRoom = this.DataContext.Get<ClassRoom>().Where(x => x.Id == classRoomId).FirstOrDefault();

            //if (classRoom == null)
            //    throw new InvalidOperationException($"ClassRoom ({classRoomId}) does not exist.");

            ClassTimetable ctt = new ClassTimetable()
            {
                Id = Guid.NewGuid(),
                SchedulingTableId = scheduleId,
                ClassRoomId = classRoomId,
                TimetableId = timetable.Id
            };

            this.DataContext.Insert<Timetable>(timetable);
            this.DataContext.Insert<ClassTimetable>(ctt);
            return ctt;
        }

        public Timetable GetTimetable(Guid scheduleId, Guid classRoomId)
        {
            Timetable tt = this.GetAll().Where(x => x.SchedulingTableId == scheduleId && x.ClassRoomId == classRoomId).Select(x=>x.Timetable).FirstOrDefault();

            return tt;
        }
    }
}
