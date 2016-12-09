using StoneCastle.Schedule.Models;
using StoneCastle.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services.Scheduler
{
    public static class TimetableHelper
    {
        public static TimetableModel Join(this TimetableModel source, TimetableModel target)
        {
            if (source == null)
                throw new ArgumentException("source");

            if (target == null)
                throw new ArgumentException("target");

            if (source.ShiftPerDay != target.ShiftPerDay)
                throw new InvalidOperationException("Cannot join Timetable with different Shift.");

            for(int i = 0; i < source.ShiftPerDay * source.SlotPerShift; i ++)
                for(int j = 0; j < Commons.Constants.DAY_OF_WEEK; j ++)
                {
                    CourseSectionSchedule s = source.TimeTableMatrix[i, j];
                    CourseSectionSchedule t = target.TimeTableMatrix[i, j];

                    if(s.ClassCourse == t.ClassCourse && t.ClassCourse == null)
                    {
                        if(t.Stage == Schedule.Models.COURSE_SECTION_STAGE.CLOSED)
                        {
                            s.Stage = t.Stage;
                        }
                    }
                }

            return source;
        }

        public static CourseSectionSchedule[,] GenerateTimeTableMatrix(this TimetableModel source)
        {
            if (source == null)
                throw new ArgumentException("source");

            int dayOfWeek = Commons.Constants.DAY_OF_WEEK;
            var timeTableMatrix = new CourseSectionSchedule[source.ShiftPerDay * source.SlotPerShift, dayOfWeek];

            /*for (int i = 0; i < source.ShiftPerDay * source.SlotPerShift; i++)
            {
                for (int j = 0; j < dayOfWeek; j++)
                {                
                    CourseSectionSchedule cs = new CourseSectionSchedule();
                    cs.Id = Guid.Empty;
                    cs.Day = (DayOfWeek)j;
                    cs.Shift = (SHIFT)(i / source.ShiftPerDay);
                    cs.Slot = (short)(i % source.SlotPerShift);
                    cs.Stage = COURSE_SECTION_STAGE.OPEN;
                    timeTable[i, j] = cs;
                }
            }*/

            foreach (CourseSectionSchedule cs in source.CourseSections)
            {
                timeTableMatrix[(int)cs.Shift * source.SlotPerShift + cs.Slot, (int)cs.Day] = cs;
            }

            return timeTableMatrix;
        }

        public static CourseSectionSchedule[,,] GenerateTimeTableMatrix2(this TimetableModel source)
        {
            if (source == null)
                throw new ArgumentException("source");

            int dayOfWeek = Commons.Constants.DAY_OF_WEEK;
            var timeTableMatrix = new CourseSectionSchedule[dayOfWeek, source.ShiftPerDay, source.SlotPerShift];

            for (int k = 0; k < dayOfWeek; k++)
            {
                for (int i = 0; i < source.ShiftPerDay ; i++)
                {
                    for (int j = 0; j < source.SlotPerShift; j++)
                    {
                        CourseSectionSchedule cs = new CourseSectionSchedule();
                        cs.Id = Guid.Empty;
                        cs.Day = (DayOfWeek)k;
                        cs.Shift = (SHIFT)(i);
                        cs.Slot = (short)(j);
                        cs.Stage = COURSE_SECTION_STAGE.OPEN;
                        timeTableMatrix[k, i, j] = cs;

                    }
                }
            }

            foreach (CourseSectionSchedule cs in source.CourseSections)
            {
                timeTableMatrix[(int)cs.Day, (int)cs.Shift, cs.Slot] = cs;
            }

            return timeTableMatrix;
        }

    }
}
