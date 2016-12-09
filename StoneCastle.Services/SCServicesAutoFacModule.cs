﻿using Autofac;

namespace StoneCastle.Services
{
    public class SCServicesAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Application.Services.ApplicationService>().As<Application.Services.IApplicationService > ();

            builder.RegisterType<Account.Services.ProfileService>().As<Account.Services.IProfileService>();
            builder.RegisterType<Account.Services.TeacherService>().As<Account.Services.ITeacherService>();

            builder.RegisterType<Organization.Services.DivisionService>().As<Organization.Services.IDivisionService>();
            builder.RegisterType<Organization.Services.ClassGroupService>().As<Organization.Services.IClassGroupService>();
            builder.RegisterType<Organization.Services.ClassRoomService>().As<Organization.Services.IClassRoomService>();
            builder.RegisterType<Organization.Services.ClassCourseService>().As<Organization.Services.IClassCourseService>();
            builder.RegisterType<Organization.Services.RoomService>().As<Organization.Services.IRoomService>();
            builder.RegisterType<Organization.Services.SemesterService>().As<Organization.Services.ISemesterService>();
            builder.RegisterType<Organization.Services.OrganizationService>().As<Organization.Services.IOrganizationService>();
            builder.RegisterType<Organization.Services.SubjectGroupService>().As<Organization.Services.ISubjectGroupService>();
            builder.RegisterType<Organization.Services.SubjectService>().As<Organization.Services.ISubjectService>();
            builder.RegisterType<Organization.Services.BuildingService>().As<Organization.Services.IBuildingService>();

            builder.RegisterType<TrainingProgram.Services.CourseService>().As<TrainingProgram.Services.ICourseService>();
            builder.RegisterType<TrainingProgram.Services.TrainingProgramService>().As<TrainingProgram.Services.ITrainingProgramService>();

            builder.RegisterType<Scheduler.ScheduleMan>().As<Scheduler.IScheduleMan>();
            builder.RegisterType<Scheduler.TimetableService>().As<Scheduler.ITimetableService>();
            builder.RegisterType<Scheduler.KatinaSchedulingService>().As<Scheduler.ISchedulingService>();

            builder.RegisterType<Schedule.Services.ScheduleService>().As<Schedule.Services.IScheduleService>();

            builder.RegisterType<Messaging.Services.MessagingMessageService>().As<Messaging.Services.IMessagingMessageService>();
            builder.RegisterType<Messaging.Services.MessagingDatabindingHelperService>().As<Messaging.Services.IMessagingDatabindingHelperService>();

            builder.RegisterType<Email.WorkingEmailService>().As<Email.IWorkingEmailService>();
            builder.RegisterType<Email.GmailProvider>().As<Email.ISendMailProvider>();
        }
    }
}