﻿using StoneCastle.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Services
{
    public static class MappingHelper
    {
        public static void ConfigureMapping()
        {
            AutoMapper.Mapper.Initialize(cfg => {

                //Entity to DTO
                cfg.CreateMap<Application.Models.Country, Application.Models.CountryModel>();
                cfg.CreateMap<Application.Models.Timezone, Application.Models.TimezoneModel>();

                cfg.CreateMap<Application.Models.User, Application.Models.UserView>();
                cfg.CreateMap<Account.Models.Profile, Account.Models.ProfileModel>();
                cfg.CreateMap<Account.Models.Account, Account.Models.AccountModel>();
                cfg.CreateMap<Account.Models.Teacher, Account.Models.TeacherModel>();

                cfg.CreateMap<Organization.Models.Organization, Organization.Models.OrganizationModel>();
                cfg.CreateMap<Organization.Models.Organization, Organization.Models.UserOrganizationModel>();
                cfg.CreateMap<Organization.Models.Division, Organization.Models.DivisionModel>();
                cfg.CreateMap<Organization.Models.SubjectGroup, Organization.Models.SubjectGroupModel>()
                            .ForMember(x=>x.Subjects, y=>y.Ignore());
                cfg.CreateMap<Organization.Models.Subject, Organization.Models.SubjectModel>();
                cfg.CreateMap<Organization.Models.Room, Organization.Models.RoomModel>();
                cfg.CreateMap<Organization.Models.ClassGroup, Organization.Models.ClassGroupModel>()
                            .ForMember(emp=>emp.TimetableId, map=>map.MapFrom(x=>x.TrainingProgram.TimetableId))
                            .ForMember(emp=>emp.TrainingProgram,
                                        map=>map.Ignore());
                cfg.CreateMap<Organization.Models.ClassRoom, Organization.Models.ClassRoomModel>();
                cfg.CreateMap<Organization.Models.Semester, Organization.Models.SemesterModel>().ForMember(x=>x.Organization, y=>y.Ignore());
                cfg.CreateMap<Organization.Models.Semester, Organization.Models.SemesterSummaryModel>();
                cfg.CreateMap<Organization.Models.ClassCourse, Organization.Models.ClassCourseModel>().ForMember(x=>x.ClassRoom, y=>y.Ignore());                
                cfg.CreateMap<Organization.Models.Building, Organization.Models.BuildingModel>();

                cfg.CreateMap<Organization.Models.ClassCourse, Organization.Models.CourseListModel>()
                            .ForMember(emp => emp.SectionPerWeek,
                                        map => map.MapFrom(p => p.Course.SectionPerWeek))
                            .ForMember(emp => emp.TeacherName,
                                        map => map.MapFrom(p => p.Teacher.Account.Profile.FirstName + " " + p.Teacher.Account.Profile.LastName))
                            .ForMember(emp => emp.SubjectName,
                                        map => map.MapFrom(p => p.Course.Subject.Name))
                            .ForMember(emp => emp.RoomName,
                                        map => map.MapFrom(p => p.Room.Name))
                            .ForMember(emp => emp.ClassRoomName,
                                        map => map.MapFrom(p => p.ClassRoom.Name));

                cfg.CreateMap<TrainingProgram.Models.Course, TrainingProgram.Models.CourseModel>();
                cfg.CreateMap<TrainingProgram.Models.TrainingProgram, TrainingProgram.Models.TrainingProgramModel>()
                            .ForMember(emp=>emp.CourseSubjects, map=>map.Ignore());

                cfg.CreateMap<Organization.Models.ClassGroup, StoneCastle.Scheduler.Models.ClassGroupSchedule>();
                cfg.CreateMap<Organization.Models.ClassRoom, StoneCastle.Scheduler.Models.ClassRoomSchedule>();
                cfg.CreateMap<Organization.Models.ClassCourse, StoneCastle.Scheduler.Models.ClassCourseSchedule>();
                cfg.CreateMap<CourseSection, StoneCastle.Scheduler.Models.CourseSectionSchedule>()
                            .ForMember(emp => emp.Timetable, map => map.Ignore());
                cfg.CreateMap<TrainingProgram.Models.Course, StoneCastle.Scheduler.Models.CourseSchedule>();
                cfg.CreateMap<Account.Models.Teacher, StoneCastle.Scheduler.Models.TeacherScheduleModel>();
                cfg.CreateMap<TrainingProgram.Models.TrainingProgram, StoneCastle.Scheduler.Models.TrainingProgramSchedule>();

                cfg.CreateMap<Schedule.Models.Timetable, StoneCastle.Scheduler.Models.TimetableModel>();
                cfg.CreateMap<Schedule.Models.SchedulingTable, StoneCastle.Schedule.Models.SchedulingTableModel>();
                cfg.CreateMap<Schedule.Models.ClassTimetable, StoneCastle.Schedule.Models.ClassTimetableModel>();
                cfg.CreateMap<Schedule.Models.SchedulingTable, StoneCastle.Schedule.Models.ScheduleStageInfo>();
                cfg.CreateMap<Schedule.Models.ScheduleEvent, StoneCastle.Schedule.Models.ScheduleEventModel>().ForMember(x => x.SchedulingTable, y => y.Ignore());

                cfg.CreateMap<Messaging.Models.MessagingMessage, Messaging.Models.MessagingMessageModel>();
                cfg.CreateMap<Messaging.Models.MessagingTemplate, Messaging.Models.MessagingTemplateModel>();
                cfg.CreateMap<Messaging.Models.MessagingTemplateContent, Messaging.Models.TemplateContentModel>()
                            .ForMember(emp=>emp.MessagingTemplateName, map=>map.MapFrom(x=>x.MessagingTemplate.MessagingTemplateName));

                //DTO to Entity
                cfg.CreateMap<Account.Models.ProfileModel, Account.Models.Profile>();
                cfg.CreateMap<Application.Models.UserView, Application.Models.User>();
                cfg.CreateMap<StoneCastle.Scheduler.Models.CourseSectionSchedule, CourseSection>();
                cfg.CreateMap<Organization.Models.ClassCourseModel, Organization.Models.ClassCourse>();
                cfg.CreateMap<StoneCastle.Scheduler.Models.ClassCourseSchedule, Organization.Models.ClassCourse>();

                cfg.CreateMap<Schedule.Models.CourseSectionModel, Schedule.Models.CourseSection>();

                cfg.CreateMap<Messaging.Models.MessagingMessageModel, Messaging.Models.MessagingMessage>();
                cfg.CreateMap<Messaging.Models.MessagingTemplateModel, Messaging.Models.MessagingTemplate>();
                cfg.CreateMap<Messaging.Models.TemplateContentModel, Messaging.Models.MessagingTemplateContent>();
            });            
        }
    }
}
