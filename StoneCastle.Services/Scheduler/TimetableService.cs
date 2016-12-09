using AutoMapper;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using StoneCastle.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services.Scheduler
{
    public class TimetableService : BaseService, ITimetableService
    {
        public const int ShiftPerDay = 2;
        public const int SlotPerShift = 5;

        public TimetableService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {

        }

        #region Helper Methods
        public TimetableModel GetTimeTable(int shifts)
        {
            TimetableModel timetable = new TimetableModel();

            CourseSectionSchedule[,] timeTableMatrix = this.GenerateTimeTableMatrix(shifts, 5, COURSE_SECTION_STAGE.OPEN);

            timetable.TimeTableMatrix = timeTableMatrix;
            timetable.ShiftPerDay = shifts;

            return timetable;
        }

        public TimetableModel GetWorkingTimeTable(int shifts, int slotPerShift)
        {
            TimetableModel timetable = new TimetableModel();

            CourseSectionSchedule[,] timeTableMatrix = this.GenerateTimeTableMatrix(shifts, slotPerShift, COURSE_SECTION_STAGE.OPEN);

            timetable.TimeTableMatrix = timeTableMatrix;
            timetable.ShiftPerDay = shifts;
            timetable.SlotPerShift = slotPerShift;

            return timetable;
        }

        private CourseSectionSchedule[,] GenerateTimeTableMatrix(int totalShift, int slotPerShift, COURSE_SECTION_STAGE stage)
        {
            int dayOfWeek = Commons.Constants.DAY_OF_WEEK;
            var timeTable = new CourseSectionSchedule[totalShift * slotPerShift, dayOfWeek];
            
            for(int i = 0; i < totalShift * slotPerShift; i ++)
            {
                for (int j = 0; j < dayOfWeek; j++)
                {
                    CourseSectionSchedule cs = new CourseSectionSchedule();
                    //cs.Id = Guid.NewGuid();
                    cs.Day = (DayOfWeek)j;
                    cs.Shift = (SHIFT) (i / slotPerShift);
                    cs.Slot = (short) (i % slotPerShift);
                    cs.Stage = stage;
                    timeTable[i, j] = cs;
                }
            }

            return timeTable;
        }

        private CourseSectionSchedule[,] GenerateWorkingTimeTableMatrix(int shifts)
        {
            int dayOfWeek = 7;
            var timeTable = new CourseSectionSchedule[shifts, dayOfWeek];

            for (int i = 0; i < shifts; i++)
            {
                for (int j = 0; j < dayOfWeek; j++)
                {
                    //Schedule.Models.SCHEDULE_TYPE type = Schedule.Models.SCHEDULE_TYPE.OPEN;

                    //if ((DayOfWeek)j == DayOfWeek.Saturday || (DayOfWeek)j == DayOfWeek.Sunday)
                        //type = Schedule.Models.SCHEDULE_TYPE.CLOSED;

                    CourseSectionSchedule cs = new CourseSectionSchedule();
                    /*cs.TimeShift = new Schedule.Models.TimeShift()
                    {
                        Id = Guid.NewGuid(),
                        Day = (DayOfWeek)j,
                        Shift = (Schedule.Models.SHIFT)i,
                        ScheduleType = type
                    };*/

                    timeTable[i, j] = cs;
                }
            }

            return timeTable;
        }
        #endregion

        #region
        public TimetableModel GetTimetable(Guid timetableId)
        {
            List<Timetable> l = this.UnitOfWork.TimetableRepository.GetAll().ToList();

            var ls = Mapper.Map<List<Schedule.Models.Timetable>, List<StoneCastle.Scheduler.Models.TimetableModel>>(l);

            return null;
        }

        public TimetableModel GetTimetable(TimetableModel model)
        {
            TimetableModel ttModel = null;

            switch (model.Type)
            {
                case TIMETABLE_TYPE.SEMESTER:
                    ttModel = this.GetSemesterTimetable(model.ReferenceObjectId);
                    break;
                case TIMETABLE_TYPE.PROGRAM:
                    ttModel = this.GetProgramTimetable(model.ReferenceObjectId);
                    break;
                case TIMETABLE_TYPE.SUBJECT:
                    ttModel = this.GetSubjectTimetable(model.ReferenceObjectId);
                    break;
                case TIMETABLE_TYPE.COURSE:
                    ttModel = this.GetCourseTimetable(model.ReferenceObjectId);
                    break;
                case TIMETABLE_TYPE.TEACHER:
                    ttModel = this.GetTeacherTimetable(model.ReferenceObjectId);
                    break;
                case TIMETABLE_TYPE.CLASS:
                    ttModel = this.GetClassTimetable(model.ReferenceObjectId);
                    break;

            };
            return ttModel;
        }

        public TimetableModel GetSemesterTimetable(Guid semesterId)
        {
            if (semesterId == null)
                throw new ArgumentNullException("semesterId");

            Timetable tt = this.UnitOfWork.SemesterRepository.GetTimetable(semesterId);

            if (tt == null)
            {
                tt = this.UnitOfWork.SemesterRepository.CreateTimetable(semesterId, ShiftPerDay, SlotPerShift);
                this.UnitOfWork.SaveChanges();
            }

            TimetableModel ttModel = Mapper.Map<Schedule.Models.Timetable, StoneCastle.Scheduler.Models.TimetableModel>(tt);
            ttModel = this.CreateTimetableMatrix(ttModel);

            return ttModel;
        }

        public TimetableModel GetProgramTimetable(Guid programId)
        {
            if (programId == null)
                throw new ArgumentNullException("programId");

            Timetable tt = this.UnitOfWork.TrainingProgramRepository.GetTimetable(programId);

            if (tt == null)
            {
                tt = this.UnitOfWork.TrainingProgramRepository.CreateTimetable(programId, ShiftPerDay, SlotPerShift);
                this.UnitOfWork.SaveChanges();
            }

            TimetableModel ttModel = Mapper.Map<Schedule.Models.Timetable, StoneCastle.Scheduler.Models.TimetableModel>(tt);
            ttModel = this.CreateTimetableMatrix(ttModel);

            return ttModel;
        }

        public TimetableModel GetSubjectTimetable(Guid subjectId)
        {
            if (subjectId == null)
                throw new ArgumentNullException("subjectId");

            Timetable tt = this.UnitOfWork.CourseRepository.GetTimetable(subjectId);

            if (tt == null)
            {
                tt = this.UnitOfWork.CourseRepository.CreateTimetable(subjectId, ShiftPerDay, SlotPerShift);
                this.UnitOfWork.SaveChanges();
            }

            TimetableModel ttModel = Mapper.Map<Schedule.Models.Timetable, StoneCastle.Scheduler.Models.TimetableModel>(tt);
            ttModel = this.CreateTimetableMatrix(ttModel);

            return ttModel;
        }

        public TimetableModel GetCourseTimetable(Guid courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException("courseId");

            Timetable tt = this.UnitOfWork.ClassCourseRepository.GetTimetable(courseId);

            if (tt == null)
            {
                tt = this.UnitOfWork.ClassCourseRepository.CreateTimetable(courseId, ShiftPerDay, SlotPerShift);
                this.UnitOfWork.SaveChanges();
            }

            TimetableModel ttModel = Mapper.Map<Schedule.Models.Timetable, StoneCastle.Scheduler.Models.TimetableModel>(tt);
            ttModel = this.CreateTimetableMatrix(ttModel);

            return ttModel;
        }

        public TimetableModel GetClassTimetable(Guid classRoomId)
        {
            throw new NotImplementedException();
        }

        public TimetableModel GetTeacherTimetable(Guid teacherId)
        {
            if (teacherId == null)
                throw new ArgumentNullException("teacherId");            

            TeacherDivision teacherDivision = this.UnitOfWork.TeacherDivisionRepository.GetByTeacherId(teacherId);

            if (teacherDivision == null)
                throw new InvalidOperationException("Teacher has not assigned to this semester.");

            Timetable tt = null;

            if (teacherDivision.Timetable != null)
            {
                tt = teacherDivision.Timetable;
            }
            else
            {
                tt = this.UnitOfWork.TeacherDivisionRepository.CreateTeacherTimetable(teacherDivision.Id, ShiftPerDay, SlotPerShift);
                this.UnitOfWork.SaveChanges();
            }

            TimetableModel ttModel = Mapper.Map<Schedule.Models.Timetable, StoneCastle.Scheduler.Models.TimetableModel>(tt);
            ttModel = this.CreateTimetableMatrix(ttModel);

            return ttModel;
        }

        public Guid SaveTimetable(TimetableModel timetable)
        {
            if (timetable == null)
                throw new ArgumentNullException("timetable");

            if (timetable.Id == null)
                throw new ArgumentNullException("timetable.Id");

            Timetable tt = this.UnitOfWork.TimetableRepository.GetById(timetable.Id);

            if (tt == null)
                throw new InvalidOperationException($"Timetable ({timetable.Id}) does not exist.");


            for (int i = 0; i < timetable.ShiftPerDay * timetable.SlotPerShift; i++)
            {
                for (int j = 0; j < Commons.Constants.DAY_OF_WEEK; j++)
                {
                    CourseSectionSchedule cs = timetable.TimeTableMatrix[i, j];                    
                    if(cs.Checked && (cs.Id == null || cs.Id == Guid.Empty))
                    {// Added
                        //CourseSection courseSection = Mapper.Map<CourseSectionSchedule, CourseSection>(cs);
                        CourseSection courseSection = new CourseSection();
                        courseSection.Day = (DayOfWeek)j;
                        courseSection.Id = Guid.NewGuid();
                        courseSection.TimetableId = timetable.Id;
                        courseSection.Stage = COURSE_SECTION_STAGE.OPEN;
                        courseSection.Shift = cs.Shift;
                        courseSection.Slot = cs.Slot;

                        this.UnitOfWork.CourseSectionRepository.Insert(courseSection);
                    }
                    else if (!cs.Checked && (cs.Id != Guid.Empty))
                    {// Remove
                        //CourseSection courseSection = Mapper.Map<CourseSectionSchedule, CourseSection>(cs);
                        this.UnitOfWork.CourseSectionRepository.DeleteCourseSection(cs.Id);
                    }
                }
            }

            this.UnitOfWork.SaveChanges();

            return tt.Id;
        }
        #endregion

        #region Private Methods
        private TimetableModel CreateTimetableMatrix(TimetableModel ttModel)
        {
            int totalShift = 2;
            int slotPerShift = 5;

            CourseSectionSchedule[,] timeTableMatrix = this.GenerateTimeTableMatrix(totalShift, slotPerShift, COURSE_SECTION_STAGE.CLOSED);
            ttModel.ShiftPerDay = totalShift;
            ttModel.SlotPerShift = slotPerShift;
            ttModel.TimeTableMatrix = timeTableMatrix;

            foreach (CourseSectionSchedule cs in ttModel.CourseSections)
            {
                cs.Stage = COURSE_SECTION_STAGE.OPEN;
                int shift = (int)cs.Shift;
                timeTableMatrix[shift * ttModel.SlotPerShift + cs.Slot, (int)cs.Day] = cs;
                cs.Checked = true;
            }

            return ttModel;
        }
        #endregion

    }
}
