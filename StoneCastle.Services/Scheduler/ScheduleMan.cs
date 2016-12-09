using AutoMapper;
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
    public class ScheduleMan : BaseService, IScheduleMan
    {
        private ITimetableService timetableService;

        private int workingShiftInDay = 2;
        private int workingSlotPerShift = 5;

        private ScheduleBoard ScheduleBoard { get; set; }

        private Random rand = new Random();

        private AutoMapper.MapperConfiguration config;

        public ScheduleMan(IUnitOfWork unitOfWork, ITimetableService timetableService) : base(unitOfWork) 
        {
            this.timetableService = timetableService;

            this.ScheduleBoard = new ScheduleBoard();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Organization.Models.ClassGroup, ClassGroupSchedule>(); //.ForMember(x => x.TrainingProgram, y => y.Ignore());
                cfg.CreateMap<TrainingProgram.Models.TrainingProgram, TrainingProgramSchedule>();
                cfg.CreateMap<Organization.Models.ClassRoom, ClassRoomSchedule>();
                cfg.CreateMap<TrainingProgram.Models.Course, CourseSchedule>().ForMember(x => x.TrainingProgram, y => y.Ignore());
                cfg.CreateMap<Schedule.Models.Timetable, TimetableModel>();
                cfg.CreateMap<ClassCourse, ClassCourseSchedule>();
                cfg.CreateMap<CourseSection, CourseSectionSchedule>();
                cfg.CreateMap<Account.Models.Teacher, TeacherScheduleModel>();
                cfg.CreateMap<Account.Models.Account, Account.Models.AccountModel>();
                cfg.CreateMap<Account.Models.Profile, Account.Models.ProfileModel>();
                cfg.CreateMap<Application.Models.User, Application.Models.UserView>();
            });

        }

        public ScheduleBoard Processing(Guid semesterId)
        {
            /*StoneCastle.Scheduler.Models.ClassGroupSchedule cg1 = this.GetRandomScheduleGroup("10", this.workingShiftInDay);
            StoneCastle.Scheduler.Models.ClassGroupSchedule cg2 = this.GetRandomScheduleGroup("11", this.workingShiftInDay);
            StoneCastle.Scheduler.Models.ClassGroupSchedule cg3 = this.GetRandomScheduleGroup("12", this.workingShiftInDay);
            this.ScheduleBoard.ScheduleGroups.Add(cg1);
            this.ScheduleBoard.ScheduleGroups.Add(cg2);
            this.ScheduleBoard.ScheduleGroups.Add(cg3);

            this.CalculateSchedule(cg1);
            this.CalculateSchedule(cg2);
            this.CalculateSchedule(cg3);

            return this.ScheduleBoard;*/

            ScheduleBoard board = this.Calculating(semesterId);

            return board;
        }

        public ScheduleBoard Calculating(Guid semesterId)
        {
            ScheduleBoard board = new ScheduleBoard();
            Logger.Debug($"Calculating semester: {semesterId}");
            // Get Class Groups
            List<ClassGroup> classGroups = this.UnitOfWork.ClassGroupRepository.GetSemesterClassGroups(semesterId).ToList();            

            var mapper = config.CreateMapper();

            List<ClassGroupSchedule> cgs = mapper.Map<List<ClassGroup>, List<ClassGroupSchedule>>(classGroups);

            Logger.Debug($"Calculating semester - Total group: {cgs.Count}");

            foreach (ClassGroupSchedule cg in cgs)
            {
                Logger.Debug($"Calculating group: {cg.Name}");
                board.ClassGroups.Add(cg);
                this.CalculateClassGroupSchedule(cg);
                this.CalculateSchedule(cg, board);
                Logger.Debug($"Complete calculating group: {cg.Name}");
            }

            Logger.Debug($"Complete calculating semester: {semesterId}");

            return board;
        }

        private void CalculateClassGroupSchedule(ClassGroupSchedule cg)
        {
            if (cg == null)
                throw new ArgumentNullException("cg");

            TrainingProgramSchedule trainingProgram = cg.TrainingProgram;

            if(trainingProgram == null)
            {
                throw new InvalidOperationException($"ClassGroup {cg.Id} does not containt training program.");
            }

            trainingProgram.Timetable = this.timetableService.GetProgramTimetable(trainingProgram.Id);

            List<Course> css = this.UnitOfWork.CourseRepository.GetCourseSubjectByTrainingProgram(trainingProgram.Id).ToList();

            var mapper = config.CreateMapper();

            List<CourseSchedule> courseSubjects = mapper.Map<List<Course>, List<CourseSchedule>>(css);
            trainingProgram.CourseSubjects = courseSubjects;

            foreach (ClassRoomSchedule cr in cg.ClassRooms)
            {
                StoneCastle.Scheduler.Models.TimetableModel tt = this.timetableService.GetWorkingTimeTable(this.workingShiftInDay, this.workingSlotPerShift);
                tt = tt.Join(trainingProgram.Timetable);

                cr.Timetable = tt;

                // Get Courses
                List<ClassCourse> courses = this.UnitOfWork.ClassCourseRepository.GetCoursesByClassRoom(cr.Id).ToList();
                List<ClassCourseSchedule> courseSchedules = mapper.Map<List<ClassCourse>, List<ClassCourseSchedule>>(courses);
                cr.Courses = courseSchedules;
            }
        }

        #region Testing with Random
        private Organization.Models.ClassGroup GetRandomClassGroup(int workingShiftInDay)
        {
            TrainingProgram.Models.TrainingProgram tp = new TrainingProgram.Models.TrainingProgram()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()
            };

            Organization.Models.ClassGroup cg = new Organization.Models.ClassGroup()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                TrainingProgram = tp,
            };

            int classCount = 0;
            while(classCount == 0)
                classCount = rand.Next(6);

            List<Organization.Models.ClassRoom> classRooms = new List<Organization.Models.ClassRoom>();
            for(int i = 0; i < classCount; i ++)
            {
                Organization.Models.ClassRoom cr = new Organization.Models.ClassRoom()
                {
                    Id = Guid.NewGuid(),
                    Name = i.ToString(),
                    ClassGroup = cg,
                };

                ((HashSet<Organization.Models.ClassRoom>)cg.ClassRooms).Add(cr);
                classRooms.Add(cr);
            }


            int courseCount = 0;
            while(courseCount == 0)
                courseCount = rand.Next(5);

            List<Account.Models.Teacher> teachers = new List<Account.Models.Teacher>();
            List<TrainingProgram.Models.Course> courseSubjects = new List<TrainingProgram.Models.Course>();

            for (int i = 0; i < courseCount; i++)
            {
                int sectionPerWeek = 0;
                while (sectionPerWeek == 0)
                    sectionPerWeek = rand.Next(3);

                // Course Subject
                TrainingProgram.Models.Course cs = new TrainingProgram.Models.Course()
                {
                    Id = Guid.NewGuid(),
                    TrainingProgram = tp,
                    SectionPerWeek = sectionPerWeek
                };

                ((HashSet<TrainingProgram.Models.Course>)tp.CourseSubjects).Add(cs);
                courseSubjects.Add(cs);

                // Teachers

                Account.Models.Teacher tc = new Account.Models.Teacher()
                {
                    Id = Guid.NewGuid(),                    
                };

                teachers.Add(tc);
            }

            for(int i = 0; i < classCount; i ++)
            {
                for (int j = 0; j < courseCount; j++)
                {
                    Organization.Models.ClassCourse c = new ClassCourse()
                    {
                        Id = Guid.NewGuid(),
                        ClassRoom = classRooms[i],
                        Teacher = teachers[j],
                        Course = courseSubjects[j]
                    };

                    ((HashSet<ClassCourse>)classRooms[i].Courses).Add(c);
                }
            }

            return cg;
        }

        private StoneCastle.Scheduler.Models.ClassGroupSchedule GetRandomScheduleGroup(string groupName, int workingShiftInDay)
        {
            TrainingProgramSchedule trainingProgram = new TrainingProgramSchedule()
            {
                Id = Guid.NewGuid(),
                Timetable = timetableService.GetWorkingTimeTable(this.workingShiftInDay, this.workingSlotPerShift)
                //Timetable = timetableService.GetTimeTable(this.workingShiftInDay)
            };

            string[] courseSubjectNames = new string[] { "Math", "History", "Geo", "English", "Art", "Literality", "Chemistry" };

            int courseSubjectCount = 0;
            while (courseSubjectCount == 0)
                courseSubjectCount = rand.Next(6);

            for (int i = 0; i < courseSubjectCount; i ++)
            {
                CourseSchedule courseSubject = new CourseSchedule()
                {
                    Id = Guid.NewGuid(),
                    //TrainingProgram = trainingProgram,
                    Name = courseSubjectNames[i],
                    SectionPerWeek = rand.Next(3),
                    HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                };

                trainingProgram.CourseSubjects.Add(courseSubject);
            }

            StoneCastle.Scheduler.Models.ClassGroupSchedule cg = new StoneCastle.Scheduler.Models.ClassGroupSchedule()
            {
                Id = Guid.NewGuid(),
                Name = groupName,
                TrainingProgram = trainingProgram
            };

            int classCount = 0;
            while (classCount == 0)
                classCount = rand.Next(6);

            List<StoneCastle.Scheduler.Models.ClassRoomSchedule> classRooms = new List<StoneCastle.Scheduler.Models.ClassRoomSchedule>();
            for (int i = 0; i < classCount; i++)
            {
                StoneCastle.Scheduler.Models.TimetableModel tt = this.timetableService.GetWorkingTimeTable(workingShiftInDay, this.workingSlotPerShift);
                tt = tt.Join(trainingProgram.Timetable);

                StoneCastle.Scheduler.Models.ClassRoomSchedule cr = new StoneCastle.Scheduler.Models.ClassRoomSchedule()
                {
                    Id = Guid.NewGuid(),
                    Name = $"{groupName}.{(i + 1)}",
                    //ClassGroup = cg,
                    Timetable = tt,
                };

                cg.ClassRooms.Add(cr);
                classRooms.Add(cr);
            }

            // Course
            for (int i = 0; i < courseSubjectCount; i++)
            {
                CourseSchedule courseSubject = ((List<CourseSchedule>)trainingProgram.CourseSubjects)[i];

                StoneCastle.Scheduler.Models.TimetableModel tt = this.timetableService.GetWorkingTimeTable(workingShiftInDay, this.workingSlotPerShift);

                TeacherScheduleModel teacher = new TeacherScheduleModel()
                {
                    Id = Guid.NewGuid(),
                    //FirstName = "Teacher",
                    //LastName = i.ToString(),
                    Timetable = tt
                };

                //for (int j = 0; j < classCount; j++)
                foreach(ClassRoomSchedule classRoom in classRooms)
                {
                    StoneCastle.Scheduler.Models.TimetableModel t = this.timetableService.GetWorkingTimeTable(workingShiftInDay, this.workingSlotPerShift);

                    ClassCourseSchedule course = new ClassCourseSchedule()
                    {
                        Id = Guid.NewGuid(),
                        Teacher = teacher,
                        Course = courseSubject,
                        Timetable = t
                    };

                    classRoom.Courses.Add(course);
                }
            }

            return cg;
        }
        #endregion

        public StoneCastle.Scheduler.Models.TimetableModel GetClassTimetable()
        {
            //return timetableService.GetTimeTable(workingShiftInDay);
            return timetableService.GetTimetable(Guid.Empty);
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
                    if(cs.Stage == COURSE_SECTION_STAGE.OPEN)
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
                do {

                    int i = rand.Next(tt.ShiftPerDay);
                    int j = rand.Next(Commons.Constants.DAY_OF_WEEK);
                    int k = rand.Next(tt.SlotPerShift);

                    CourseSectionSchedule cs = tt.TimeTableMatrix[i * tt.SlotPerShift + k, j];
                    if (cs.Stage == COURSE_SECTION_STAGE.OPEN)
                    {
                        TimeShift tf = new TimeShift()
                        {
                            Day = (DayOfWeek) j,
                            Shift = i,
                            Slot = k
                        };

                        if (!this.IsTimeShiftExistInList(tf, checklist))
                        {
                            checklist.Add(tf);

                            // Check exiting teacher section booked
                            if (this.CanBeBookedForTeacher(course.Teacher, tf, board))
                            {
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
            foreach(TimeShift t in list)
            {
                if (t.Day == tf.Day && t.Shift == tf.Shift && t.Slot == tf.Slot)
                    return true;
            }

            return false;
        }

        private bool CanBeBookedForTeacher(TeacherScheduleModel teacher, TimeShift tf, ScheduleBoard board)
        {
            foreach(ClassGroupSchedule cg in board.ClassGroups)
            {
                foreach(ClassRoomSchedule cr in cg.ClassRooms)
                {
                    StoneCastle.Scheduler.Models.TimetableModel tt = cr.Timetable;

                    if (this.IsTeacherAlreadyBooked(teacher, tf, tt))
                        return false;
                }
            }

            return true;
        }

        private bool IsTeacherAlreadyBooked(TeacherScheduleModel teacher, TimeShift tf, StoneCastle.Scheduler.Models.TimetableModel tt)
        {
            if (teacher == null || tf == null || tt == null) return false;

            CourseSectionSchedule cs = tt.TimeTableMatrix[tf.Shift*tf.Slot + tf.Slot, (int)tf.Day];
            ClassCourseSchedule course = cs.ClassCourse;

            if (cs.Stage == COURSE_SECTION_STAGE.BOOKED && course != null && course.Teacher != null && course.Teacher.Id == teacher.Id)
            {
                return true;
            }

            return false;
        }
    }
}
