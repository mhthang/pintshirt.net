using AutoMapper;
using StoneCastle.Account.Models;
using StoneCastle.Domain;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using StoneCastle.Scheduler.Models;
using StoneCastle.TrainingProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services.Scheduler
{
    public class KatinaSchedulingService : BaseService, ISchedulingService
    {
        private ITimetableService timetableService;

        private ScheduleBoard ScheduleBoard { get; set; }

        private Random rand = new Random();

        private AutoMapper.MapperConfiguration config;

        public KatinaSchedulingService(IUnitOfWork unitOfWork, ITimetableService timetableService) : base(unitOfWork) 
        {
            this.timetableService = timetableService;

            this.ScheduleBoard = new ScheduleBoard();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Organization.Models.ClassGroup, ClassGroupSchedule>(); //.ForMember(x => x.TrainingProgram, y => y.Ignore());
                cfg.CreateMap<TrainingProgram.Models.TrainingProgram, TrainingProgramSchedule>();
                cfg.CreateMap<Organization.Models.ClassRoom, ClassRoomSchedule>().ForMember(x => x.Courses, y => y.Ignore()); ;
                cfg.CreateMap<TrainingProgram.Models.Course, CourseSchedule>().ForMember(x => x.TrainingProgram, y => y.Ignore());
                cfg.CreateMap<Schedule.Models.Timetable, TimetableModel>();
                cfg.CreateMap<ClassCourse, ClassCourseSchedule>(); //.ForMember(x => x.ClassRoom, y => y.Ignore()); ;
                cfg.CreateMap<CourseSection, CourseSectionSchedule>().ForMember(x => x.Timetable, y => y.Ignore());
                cfg.CreateMap<Schedule.Models.ScheduleEvent, StoneCastle.Schedule.Models.ScheduleEventModel>().ForMember(x => x.SchedulingTable, y => y.Ignore());
                cfg.CreateMap<Account.Models.Teacher, StoneCastle.Scheduler.Models.TeacherScheduleModel>().ForMember(dest=> dest.FullName, opt=>opt.MapFrom(src=>$"{src.Account.Profile.FirstName} {src.Account.Profile.LastName}"));
                cfg.CreateMap<Account.Models.Account, Account.Models.AccountModel>();
                cfg.CreateMap<Account.Models.Profile, Account.Models.ProfileModel>();
                cfg.CreateMap<Application.Models.User, Application.Models.UserView>();
            });

        }

        #region Overload

        public Schedule.Models.ScheduleStageInfo ValidateScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start validating schedule board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            this.UnitOfWork.SchedulingTableRepository.ChangedValidated(scheduleId);
            this.UnitOfWork.SaveChanges();

            Logger.Debug($"Complete validating schedule board: {scheduleId}");

            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            ScheduleStageInfo scheduleStageInfo = Mapper.Map<Schedule.Models.SchedulingTable, Schedule.Models.ScheduleStageInfo>(schedule);

            return scheduleStageInfo;
        }

        public Schedule.Models.ScheduleStageInfo AdjustScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start adjusting schedule board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            this.UnitOfWork.SchedulingTableRepository.ChangeAdjust(scheduleId);

            this.UnitOfWork.SaveChanges();

            Logger.Debug($"Complete adjusting schedule board: {scheduleId}");

            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            ScheduleStageInfo scheduleStageInfo = Mapper.Map<Schedule.Models.SchedulingTable, Schedule.Models.ScheduleStageInfo>(schedule);

            return scheduleStageInfo;
        }

        public Schedule.Models.ScheduleStageInfo CompleteScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start completing schedule board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            this.UnitOfWork.SchedulingTableRepository.ChangeCompleted(scheduleId);

            this.UnitOfWork.SaveChanges();

            Logger.Debug($"Complete schedule board: {scheduleId}");

            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            ScheduleStageInfo scheduleStageInfo = Mapper.Map<Schedule.Models.SchedulingTable, Schedule.Models.ScheduleStageInfo>(schedule);

            return scheduleStageInfo;
        }

        public ScheduleBoard GenerateScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start generating schedule board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            Schedule.Models.SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            if(schedule == null)
            {
                Logger.Error($"Schedule ({scheduleId}) does not exist.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not exist.");
            }            

            Guid semesterId = schedule.SemesterId;

            if (semesterId == null || semesterId == Guid.Empty)
            {
                Logger.Error($"Schedule ({scheduleId}) does not content SemesterId.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not content SemesterId.");
            }

            ScheduleBoard board = new ScheduleBoard();
            board.Id = scheduleId;

            Logger.Debug($"Start calculating schedule-board for semester: {semesterId}");

            // Get Class Groups
            List<ClassGroup> classGroups = this.UnitOfWork.ClassGroupRepository.GetSemesterClassGroups(semesterId).ToList();

            Logger.Debug($"Found {classGroups.Count} class-groups");

            var mapper = config.CreateMapper();

            List<ClassGroupSchedule> cgs = mapper.Map<List<ClassGroup>, List<ClassGroupSchedule>>(classGroups);

            Logger.Debug($"Start calculating schedule for {cgs.Count} class-group.");

            foreach (ClassGroupSchedule cg in cgs)
            {
                Logger.Debug($"Calculating group: {cg.Name}");
                board.ClassGroups.Add(cg);
                this.CalculateClassGroupSchedule(cg);
                this.CalculateSchedule(cg, board);
                Logger.Debug($"Complete calculating group: {cg.Name}");
            }

            this.UnitOfWork.SaveChanges();

            Logger.Debug($"Complete calculating semester: {semesterId}");            

            return board;
        }

        public ScheduleBoard ProceedGeneratingScheduleBoard(Guid scheduleId)
        {
            this.UnitOfWork.SchedulingTableRepository.ChangeGeneratingStarted(scheduleId);
            Logger.Debug($"Changed status of schedule to GENERATING: {scheduleId}");
            this.UnitOfWork.SaveChanges();

            this.ClearScheduleBoard(scheduleId);
            Logger.Debug($"Cleared/Reset schedule-board: {scheduleId}");

            ScheduleBoard board = this.GenerateScheduleBoard(scheduleId);
            this.SaveScheduleBoard(board);

            this.UnitOfWork.SchedulingTableRepository.ChangeGeneratingCompleted(scheduleId);
            Logger.Debug($"Changed status of schedule to GENERATED: {scheduleId}");

            this.UnitOfWork.SaveChanges();

            return board;
        }

        public ScheduleBoard LoadScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start loading schedule board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            Schedule.Models.SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            if (schedule == null)
            {
                Logger.Error($"Schedule ({scheduleId}) does not exist.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not exist.");
            }

            Guid semesterId = schedule.SemesterId;

            if (semesterId == null || semesterId == Guid.Empty)
            {
                Logger.Error($"Schedule ({scheduleId}) does not content SemesterId.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not content SemesterId.");
            }

            ScheduleBoard board = new ScheduleBoard();
            board.Id = scheduleId;

            Logger.Debug($"Start loading schedule-board for semester: {semesterId}");

            // Get Class Groups
            List<ClassGroup> classGroups = this.UnitOfWork.ClassGroupRepository.GetSemesterClassGroups(semesterId).ToList();

            Logger.Debug($"Found {classGroups.Count} class-groups");

            var mapper = config.CreateMapper();

            List<ClassGroupSchedule> cgs = mapper.Map<List<ClassGroup>, List<ClassGroupSchedule>>(classGroups);

            foreach (ClassGroupSchedule cg in cgs)
            {
                Logger.Debug($"Loading group: {cg.Name}");
                board.ClassGroups.Add(cg);

                foreach(ClassRoomSchedule cr in cg.ClassRooms)
                {
                    Timetable tt = this.UnitOfWork.ClassTimetableRepository.GetTimetable(board.Id, cr.Id);
                    if (tt != null)
                    {
                        TimetableModel ttm = mapper.Map<Timetable, TimetableModel>(tt);

                        cr.Timetable = ttm;
                        cr.Timetable.TimeTableMatrix = ttm.GenerateTimeTableMatrix();
                    }

                    List<ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.GetCoursesByClassRoom(cr.Id).ToList();
                    cr.Courses = mapper.Map<List<ClassCourse>, List<ClassCourseSchedule>>(courses);
                }

                Logger.Debug($"Complete loading group: {cg.Name}");
            }

            Logger.Debug($"Complete loading semester: {semesterId}");

            return board;           
        }

        public void SaveScheduleBoard(ScheduleBoard scheduleBoard)
        {
            if (scheduleBoard == null)
            {
                Logger.Error($"scheduleBoard is empty.");
                throw new ArgumentNullException("scheduleBoard");
            }

            Logger.Debug($"Start saving schedule board: {scheduleBoard.Id}");

            Schedule.Models.SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleBoard.Id);

            if (schedule == null)
            {
                Logger.Error($"Schedule ({scheduleBoard.Id}) does not exist.");
                throw new InvalidOperationException($"Schedule ({scheduleBoard.Id}) does not exist.");
            }

            var mapper = config.CreateMapper();

            foreach (ClassGroupSchedule cg in scheduleBoard.ClassGroups)
            {
                foreach(ClassRoomSchedule crs in cg.ClassRooms)
                {
                    Timetable tt = new Timetable()
                    {
                        Id = Guid.NewGuid(),
                        Name = crs.Name,
                        ShiftPerDay = crs.Timetable.ShiftPerDay,
                        SlotPerShift = crs.Timetable.SlotPerShift,                        
                    };

                    this.UnitOfWork.ClassTimetableRepository.Create(scheduleBoard.Id, crs.Id, tt);

                    for (int i = 0; i < crs.Timetable.ShiftPerDay * crs.Timetable.SlotPerShift; i++)
                    {
                        for (int j = 0; j < Commons.Constants.DAY_OF_WEEK; j++)
                        {
                            CourseSectionSchedule cs = crs.Timetable.TimeTableMatrix[i, j];

                            if (cs != null && (cs.Id != null && cs.Id != Guid.Empty) && (cs.ClassCourse != null && (cs.ClassCourse.Id != null && cs.ClassCourse.Id != Guid.Empty)))
                            {// Added
                                //CourseSection courseSection = mapper.Map<CourseSectionSchedule, CourseSection>(cs);
                                CourseSection courseSection = new CourseSection();
                                courseSection.Day = (DayOfWeek)j;
                                courseSection.Id = Guid.NewGuid();
                                courseSection.TimetableId = tt.Id;
                                courseSection.ClassCourseId = cs.ClassCourse.Id;
                                courseSection.Stage = cs.Stage;
                                courseSection.Shift = cs.Shift;
                                courseSection.Slot = cs.Slot;

                                this.UnitOfWork.CourseSectionRepository.Insert(courseSection);
                            }
                        }
                    }

                }
            }

            this.UnitOfWork.SaveChanges();

            Logger.Debug($"Complete saving schedule board: {scheduleBoard.Id}");
        }

        public void ClearScheduleBoard(Guid scheduleId)
        {
            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is null.");
                throw new ArgumentNullException("scheduleId");
            }

            this.UnitOfWork.SchedulingTableRepository.Reset(scheduleId);
            this.UnitOfWork.SaveChanges();
        }

        public ScheduleStageInfo CalculateSemesterScheduleBoard(Guid semesterId)
        {
            Schedule.Models.SchedulingTable scheduleTable = this.UnitOfWork.SchedulingTableRepository.GetSemesterDefaultSchedule(semesterId);

            Guid scheduleId = Guid.Empty;

            if (scheduleTable == null)
            {
                scheduleTable = this.UnitOfWork.SchedulingTableRepository.CreateSchedule(semesterId);
                this.UnitOfWork.SaveChanges();
            }

            scheduleId = scheduleTable.Id;

            this.UnitOfWork.SchedulingTableRepository.ChangedValidated(scheduleId);
            Logger.Debug($"Changed status of schedule to VALIDATED: {scheduleId}");
            this.UnitOfWork.SaveChanges();

            this.UnitOfWork.SchedulingTableRepository.ChangeGeneratingStarted(scheduleId);
            Logger.Debug($"Changed status of schedule to GENERATING: {scheduleId}");
            this.UnitOfWork.SaveChanges();

            this.ClearScheduleBoard(scheduleId);
            Logger.Debug($"Cleared/Reset schedule-board: {scheduleId}");

            ScheduleBoard board = this.GenerateScheduleBoard(scheduleId);
            this.SaveScheduleBoard(board);

            this.UnitOfWork.SchedulingTableRepository.ChangeGeneratingCompleted(scheduleId);
            Logger.Debug($"Changed status of schedule to GENERATED: {scheduleId}");

            this.UnitOfWork.SaveChanges();

            SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            ScheduleStageInfo scheduleStageInfo = Mapper.Map<Schedule.Models.SchedulingTable, Schedule.Models.ScheduleStageInfo>(schedule);

            return scheduleStageInfo;
        }

        public void SaveScheduleBoard(UpdateTimetableBoardModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.CourseSections == null)
                throw new ArgumentNullException("model.CourseSections");

            foreach(CourseSectionModel csModel in model.CourseSections)
            {
                CourseSection cs =  new CourseSection()
                {
                    ClassCourseId = csModel.ClassCourseId,
                    Id = csModel.Id,
                    Day = csModel.Day,
                    Shift = csModel.Shift,
                    Slot = csModel.Slot,
                    TimetableId = csModel.TimetableId
                };

                if(cs.Id == Guid.Empty)
                {
                    if (csModel.ClassCourse != null)
                    {
                        cs.Id = Guid.NewGuid();
                        cs.ClassCourseId = csModel.ClassCourse.Id;

                        this.UnitOfWork.CourseSectionRepository.Insert(cs);
                    }
                }
                else
                {
                    this.UnitOfWork.CourseSectionRepository.Update(cs, x=>x.ClassCourseId);
                }
            }

            if (model.CourseSections.Count > 0)
                this.UnitOfWork.SaveChanges();
        }
        #endregion

        #region Teacher Schedule Board
        public TeacherSchedule LoadTeacherScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start loading teacher schedule-board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            Schedule.Models.SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            if (schedule == null)
            {
                Logger.Error($"Schedule ({scheduleId}) does not exist.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not exist.");
            }

            Guid semesterId = schedule.SemesterId;

            if (semesterId == null || semesterId == Guid.Empty)
            {
                Logger.Error($"Schedule ({scheduleId}) does not content SemesterId.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not content SemesterId.");
            }

            TeacherSchedule board = new TeacherSchedule();
            board.Id = scheduleId;
            board.WorkingDays = (int)Commons.Constants.DAY_OF_WEEK;
            board.ShiftPerDay = 2;
            board.SlotPerShift = 5;

            Logger.Debug($"Start loading teacher schedule-board for semester: {semesterId}");

            // Get Class Groups
            List<Teacher> teachers = this.UnitOfWork.TeacherRepository.GetSemesterTeacher(semesterId).ToList();

            Logger.Debug($"Found {teachers.Count} teachers");

            var mapper = config.CreateMapper();

            List<StoneCastle.Scheduler.Models.TeacherScheduleModel> teacherModels = mapper.Map<List<Teacher>, List<StoneCastle.Scheduler.Models.TeacherScheduleModel>>(teachers);

            foreach (StoneCastle.Scheduler.Models.TeacherScheduleModel teacher in teacherModels)
            {
                Logger.Debug($"Loading teacher: {teacher.Account.Profile.FullName}");
                board.Teachers.Add(teacher);

                Timetable tt = this.UnitOfWork.TeacherRepository.GetTeacherTimetable(teacher.Id, scheduleId);

                teacher.Timetable = mapper.Map<Timetable, TimetableModel>(tt);

                teacher.Timetable.TimeTableMatrix2 = teacher.Timetable.GenerateTimeTableMatrix2();                

                Logger.Debug($"Complete loading teacher schedule: {teacher.Account.Profile.FullName}");
            }

            Logger.Debug($"Complete loading teacher schedule for semester: {semesterId}");

            return board;
        }
        #endregion

        #region Class Schedule Board
        public ClassScheduleBoard LoadClassScheduleBoard(Guid scheduleId)
        {
            Logger.Debug($"Start loading class schedule-board: {scheduleId}");

            if (scheduleId == null || scheduleId == Guid.Empty)
            {
                Logger.Error($"ScheduleId is empty.");
                throw new ArgumentNullException("scheduleId");
            }

            Schedule.Models.SchedulingTable schedule = this.UnitOfWork.SchedulingTableRepository.GetById(scheduleId);

            if (schedule == null)
            {
                Logger.Error($"Schedule ({scheduleId}) does not exist.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not exist.");
            }

            Guid semesterId = schedule.SemesterId;

            if (semesterId == null || semesterId == Guid.Empty)
            {
                Logger.Error($"Schedule ({scheduleId}) does not content SemesterId.");
                throw new InvalidOperationException($"Schedule ({scheduleId}) does not content SemesterId.");
            }

            ClassScheduleBoard board = new ClassScheduleBoard();
            board.Id = scheduleId;
            board.WorkingDays = (int)Commons.Constants.DAY_OF_WEEK;
            board.ShiftPerDay = 2;
            board.SlotPerShift = 5;

            Logger.Debug($"Start loading schedule-board for semester: {semesterId}");

            // Get Class Groups
            List<ClassGroup> classGroups = this.UnitOfWork.ClassGroupRepository.GetSemesterClassGroups(semesterId).ToList();

            Logger.Debug($"Found {classGroups.Count} class-groups");

            var mapper = config.CreateMapper();

            List<ClassGroupSchedule> cgs = mapper.Map<List<ClassGroup>, List<ClassGroupSchedule>>(classGroups);

            foreach (ClassGroupSchedule cg in cgs)
            {
                Logger.Debug($"Loading group: {cg.Name}");
                board.ClassGroups.Add(cg);

                foreach (ClassRoomSchedule cr in cg.ClassRooms)
                {
                    Timetable tt = this.UnitOfWork.ClassTimetableRepository.GetTimetable(board.Id, cr.Id);
                    if (tt != null)
                    {
                        TimetableModel ttm = mapper.Map<Timetable, TimetableModel>(tt);

                        cr.Timetable = ttm;
                        cr.Timetable.TimeTableMatrix = ttm.GenerateTimeTableMatrix();
                    }

                    List<ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.GetCoursesByClassRoom(cr.Id).ToList();
                    cr.Courses = mapper.Map<List<ClassCourse>, List<ClassCourseSchedule>>(courses);
                }

                Logger.Debug($"Complete loading group: {cg.Name}");
            }

            Logger.Debug($"Complete loading semester: {semesterId}");

            return board;
        }
        #endregion

        #region Private Methods - Generating
        private void CalculateClassGroupSchedule(ClassGroupSchedule cg)
        {
            if (cg == null)
            {
                Logger.Error($"Null exception: cg");
                throw new ArgumentNullException("cg");
            }
            TrainingProgramSchedule trainingProgram = cg.TrainingProgram;

            if (trainingProgram == null)
            {
                Logger.Error($"Class-group does not containt Training Program.");
                throw new InvalidOperationException($"ClassGroup {cg.Id} does not containt training program.");
            }

            trainingProgram.Timetable = this.timetableService.GetProgramTimetable(trainingProgram.Id);

            List<Course> css = this.UnitOfWork.CourseRepository.GetCourseSubjectByTrainingProgram(trainingProgram.Id).ToList();

            var mapper = config.CreateMapper();

            List<CourseSchedule> courseSubjects = mapper.Map<List<Course>, List<CourseSchedule>>(css);
            trainingProgram.CourseSubjects = courseSubjects;

            int shiftPerDay = trainingProgram.Timetable.ShiftPerDay;
            int slotPerShift = trainingProgram.Timetable.SlotPerShift;

            foreach (ClassRoomSchedule cr in cg.ClassRooms)
            {
                StoneCastle.Scheduler.Models.TimetableModel tt = this.timetableService.GetWorkingTimeTable(shiftPerDay, slotPerShift);
                tt = tt.Join(trainingProgram.Timetable);

                cr.Timetable = tt;

                // Get Courses
                List<ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.GetCoursesByClassRoom(cr.Id).ToList();
                List<ClassCourseSchedule> courseSchedules = mapper.Map<List<ClassCourse>, List<ClassCourseSchedule>>(courses);
                cr.Courses = courseSchedules;
            }
        }

        private void CalculateSchedule(ClassGroupSchedule classGroup, ScheduleBoard board)
        {
            foreach (ClassRoomSchedule classRoom in classGroup.ClassRooms)
            {
                //foreach (CourseSubjectSchedule courseSubject in classGroup.TrainingProgram.CourseSubjects)
                foreach (ClassCourseSchedule course in classRoom.Courses)
                {
                    StoneCastle.Scheduler.Models.TimetableModel tt = classRoom.Timetable;
                    this.CalculateTimetable(tt, course, board);
                }
                Logger.Debug($"Calculated class: {classRoom.Name}");
            }
        }

        private void CalculateTimetable(StoneCastle.Scheduler.Models.TimetableModel tt, ClassCourseSchedule course, ScheduleBoard board)
        {
            if (course.Course.SectionPerWeek == 0) return;

            bool canSchedule = false;

            // Check tt has open-shifts for courses
            int openSlotCount = 0;
            for (int i = 0; i < tt.ShiftPerDay * tt.SlotPerShift; i++)
            {
                for (int j = 0; j < Commons.Constants.DAY_OF_WEEK; j++)
                {
                    CourseSectionSchedule cs = tt.TimeTableMatrix[i, j];
                    if (cs.Stage == COURSE_SECTION_STAGE.OPEN)
                    {
                        openSlotCount++;
                    }
                }
            }

            if (openSlotCount >= course.Course.SectionPerWeek)
            {
                canSchedule = true;
            }

            if (canSchedule)
            {
                List<TimeShift> checklist = new List<TimeShift>();

                int bookedCount = 0;
                do
                {

                    int i = rand.Next(tt.ShiftPerDay);
                    int j = rand.Next(Commons.Constants.DAY_OF_WEEK);
                    int k = rand.Next(tt.SlotPerShift);

                    CourseSectionSchedule cs = tt.TimeTableMatrix[i * tt.SlotPerShift + k, j];
                    if (cs.Stage == COURSE_SECTION_STAGE.OPEN)
                    {
                        TimeShift tf = new TimeShift()
                        {
                            Day = (DayOfWeek)j,
                            Shift = i,
                            Slot = k
                        };

                        if (!this.IsTimeShiftExistInList(tf, checklist))
                        {
                            checklist.Add(tf);

                            // Check exiting teacher section booked
                            if (this.CanBeBookedForTeacher(course.Teacher, tf, board))
                            {
                                if (cs.Id == null || cs.Id == Guid.Empty)
                                    cs.Id = Guid.NewGuid();

                                cs.ClassCourse = course;
                                cs.Stage = COURSE_SECTION_STAGE.BOOKED;
                                bookedCount++;
                            }
                        }
                    }

                } while (bookedCount < course.Course.SectionPerWeek && checklist.Count < openSlotCount);

            }
        }

        private bool IsTimeShiftExistInList(TimeShift tf, List<TimeShift> list)
        {
            if (tf == null || list == null)
                return false;
            foreach (TimeShift t in list)
            {
                if (t.Day == tf.Day && t.Shift == tf.Shift && t.Slot == tf.Slot)
                    return true;
            }

            return false;
        }

        private bool CanBeBookedForTeacher(StoneCastle.Scheduler.Models.TeacherScheduleModel teacher, TimeShift tf, ScheduleBoard board)
        {
            foreach (ClassGroupSchedule cg in board.ClassGroups)
            {
                foreach (ClassRoomSchedule cr in cg.ClassRooms)
                {
                    StoneCastle.Scheduler.Models.TimetableModel tt = cr.Timetable;

                    if (this.IsTeacherAlreadyBooked(teacher, tf, tt))
                        return false;
                }
            }

            return true;
        }

        private bool IsTeacherAlreadyBooked(StoneCastle.Scheduler.Models.TeacherScheduleModel teacher, TimeShift tf, StoneCastle.Scheduler.Models.TimetableModel tt)
        {
            if (teacher == null || tf == null || tt == null) return false;

            CourseSectionSchedule cs = tt.TimeTableMatrix[tf.Shift * tf.Slot + tf.Slot, (int)tf.Day];
            ClassCourseSchedule course = cs.ClassCourse;

            if (cs.Stage == COURSE_SECTION_STAGE.BOOKED && course != null && course.Teacher != null && course.Teacher.Id == teacher.Id)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods - Saving

        #endregion
    }
}
