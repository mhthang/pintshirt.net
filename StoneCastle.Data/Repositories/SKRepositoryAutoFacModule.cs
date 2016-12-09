using Autofac;
using StoneCastle.Application;
using StoneCastle.Application.Repositories;
using StoneCastle.Account;
using StoneCastle.Account.Repositories;
using StoneCastle.Data.EntityFramework;
using StoneCastle.Domain;
using StoneCastle.Organization;
using StoneCastle.Organization.Repositories;
using StoneCastle.Schedule;
using StoneCastle.Schedule.Repositories;
using StoneCastle.TrainingProgram;
using StoneCastle.TrainingProgram.Repositories;

namespace StoneCastle.Data.Repositories
{
    public class SKRepositoryAutoFacModule: Module
    {
        private readonly string _connStr;

        public SKRepositoryAutoFacModule(string connString)
        {
            this._connStr = connString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SCDataContext(_connStr)).As<ISCDataContext>().InstancePerRequest();

            #region Application
            builder.RegisterType<CountryRepository>().As<ICountryRepository>();
            builder.RegisterType<TimezoneRepository>().As<ITimezoneRepository>();

            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();

            builder.RegisterType<ClassGroupRepository>().As<IClassGroupRepository>();
            builder.RegisterType<ClassRoomRepository>().As<IClassRoomRepository>();
            builder.RegisterType<ClassCourseRepository>().As<IClassCourseRepository>();
            builder.RegisterType<DivisionRepository>().As<IDivisionRepository>();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
            builder.RegisterType<SemesterRepository>().As<ISemesterRepository>();
            builder.RegisterType<SubjectGroupRepository>().As<ISubjectGroupRepository>();
            builder.RegisterType<SubjectRepository>().As<ISubjectRepository>();
            builder.RegisterType<BuildingRepository>().As<IBuildingRepository>();

            builder.RegisterType<TimetableRepository>().As<ITimetableRepository>();
            builder.RegisterType<ClassTimetableRepository>().As<IClassTimetableRepository>();

            builder.RegisterType<CourseSectionRepository>().As<ICourseSectionRepository>();
            builder.RegisterType<SchedulingTableRepository>().As<ISchedulingTableRepository>();

            builder.RegisterType<CourseRepository>().As<ICourseRepository>();
            builder.RegisterType<TrainingProgramRepository>().As<ITrainingProgramRepository>();

            builder.RegisterType<SchedulingTableRepository>().As<ISchedulingTableRepository>();

            #endregion

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
