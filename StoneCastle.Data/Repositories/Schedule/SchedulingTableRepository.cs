using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Schedule.Repositories
{
    public class SchedulingTableRepository : Repository<Schedule.Models.SchedulingTable, System.Guid>, ISchedulingTableRepository
    {
        public SchedulingTableRepository(ISCDataContext context) : base(context)
        {
        }

        public List<Models.SchedulingTable> SearchSemesterSchedule(Guid semesterId, string filter, int pageIndex, int pageSize)
        {
            return this.GetAll().Where(x => x.SemesterId == semesterId).ToList();
        }

        public Models.SchedulingTable CreateSchedule(Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            if (semesterId == null || semesterId == System.Guid.Empty)
                throw new ArgumentNullException("semesterId");

            Models.SchedulingTable schedule = new Models.SchedulingTable()
            {
                Id = Guid.NewGuid(),
                SemesterId = semesterId,
                Name = name,
                ShortName = shortName,
                HighlightColor = HighlightColor,
                LogoUrl = logoUrl,
                Stage = Models.SCHEDULE_STAGE.NEW,
                IsActive = isActive,
                IsDeleted = false,
                AddedStamp = DateTime.UtcNow
            };


            Models.ScheduleEvent scheduleEvent = new Models.ScheduleEvent()
            {
                Id = Guid.NewGuid(),
                Stage = Models.SCHEDULE_STAGE.NEW,
                Timestamp = DateTime.Now,
                SchedulingTableId = schedule.Id
            };

            this.DataContext.Insert<Models.SchedulingTable>(schedule);
            this.DataContext.Insert<Models.ScheduleEvent>(scheduleEvent);

            return schedule;
        }

        public Models.SchedulingTable CreateSchedule(Guid semesterId)
        {
            Random rand = new Random();
            string highlightColor = Commons.Ultility.GetHighlightColor(rand);
            return this.CreateSchedule(semesterId, null, null, highlightColor, null, true);
        }

        public Models.SchedulingTable UpdateSchedule(Guid id, Guid semesterId, string name, string shortName, string HighlightColor, string logoUrl, bool isActive)
        {
            Models.SchedulingTable schedule = this.GetById(id);

            if (schedule == null)
                throw new InvalidOperationException($"Schedule ({id}) does not exist.");

            schedule.Name = name;
            schedule.ShortName = shortName;
            schedule.HighlightColor = HighlightColor;
            schedule.LogoUrl = logoUrl;
            schedule.IsActive = isActive;
            schedule.ChangedStamp = DateTime.UtcNow;

            this.DataContext.Update<Models.SchedulingTable, Guid>(schedule, x => x.SemesterId, x => x.Name, x => x.ShortName, x => x.HighlightColor, x => x.LogoUrl, x => x.IsActive, x=>x.ChangedStamp);

            return schedule;
        }

        public void ChangedValidated(Guid scheduleId)
        {
            this.ChangeScheduleStage(scheduleId, Models.SCHEDULE_STAGE.VALIDATED);
        }

        public void ChangeGeneratingStarted(Guid scheduleId)
        {
            this.ChangeScheduleStage(scheduleId, Models.SCHEDULE_STAGE.GENERATING);
        }

        public void ChangeGeneratingCompleted(Guid scheduleId)
        {
            this.ChangeScheduleStage(scheduleId, Models.SCHEDULE_STAGE.GENERATED);
        }

        public void ChangeAdjust(Guid scheduleId)
        {
            this.ChangeScheduleStage(scheduleId, Models.SCHEDULE_STAGE.ADJUSTMENT);
        }

        public void ChangeCompleted(Guid scheduleId)
        {
            this.ChangeScheduleStage(scheduleId, Models.SCHEDULE_STAGE.COMPLETED);
        }

        protected void ChangeScheduleStage(Guid scheduleId, Models.SCHEDULE_STAGE stage)
        {
            Models.SchedulingTable schedule = this.GetById(scheduleId);

            if (schedule == null)
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not exist.");

            if (!schedule.IsActive)
                throw new InvalidOperationException($"Cannot change stage of Un-Actice Schedule ({scheduleId})");

            if (schedule.IsDeleted)
                throw new InvalidOperationException($"Cannot change stage of Deleted Schedule ({scheduleId})");

            Models.ScheduleEvent scheduleEvent = new Models.ScheduleEvent()
            {
                Id = Guid.NewGuid(),
                Stage = stage,
                Timestamp = DateTime.Now,
                SchedulingTableId = schedule.Id
            };

            this.DataContext.Insert<Models.ScheduleEvent>(scheduleEvent);

            schedule.Stage = stage;
            this.DataContext.Update<Models.SchedulingTable, Guid>(schedule, x=>x.Stage);
        }

        public void Reset(Guid scheduleId)
        {
            if(scheduleId == null || scheduleId == Guid.Empty)
            {
                throw new ArgumentNullException("ScheduleId");
            }

            Models.SchedulingTable schedule = this.GetById(scheduleId);

            if (schedule == null)
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not exist.");

            if (!schedule.IsActive)
                throw new InvalidOperationException($"Cannot change stage of Un-Actice Schedule ({scheduleId})");

            if (schedule.IsDeleted)
                throw new InvalidOperationException($"Cannot change stage of Deleted Schedule ({scheduleId})");

            List<Guid> classTimetableIds = this.DataContext.Get<Models.ClassTimetable>().Where(x => x.SchedulingTableId == scheduleId).Select(x => x.Id).ToList();
            List<Guid> timetableIds = this.DataContext.Get<Models.ClassTimetable>().Where(x => x.SchedulingTableId == scheduleId).Select(x => x.TimetableId).ToList();
            List<Guid> courseSectionIds = this.DataContext.Get<Models.CourseSection>().Where(x => timetableIds.Contains(x.TimetableId)).Select(x => x.Id).ToList();

            // Remove all CourseSections
            foreach(Guid id in courseSectionIds)
                this.DataContext.Delete<Models.CourseSection>(id);

            // Remove all Timetables
            foreach (Guid id in timetableIds)
                this.DataContext.Delete<Models.Timetable>(id);

            // Remove all classTimetables
            foreach (Guid id in classTimetableIds)
                this.DataContext.Delete<Models.ClassTimetable>(id);
        }

        public Models.SchedulingTable GetSemesterDefaultSchedule(Guid semesterId)
        {
            if (semesterId == null || semesterId == Guid.Empty)
                throw new ArgumentNullException("semesterId");

            return this.GetAll().Where(x => x.SemesterId == semesterId && x.IsActive && !x.IsDeleted).FirstOrDefault();
        }
    }
}
