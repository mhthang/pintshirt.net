using Microsoft.AspNet.Identity.EntityFramework;
using StoneCastle.Account.Models;
using StoneCastle.Application.Models;
using StoneCastle.Domain.Authentication.Entities;
using StoneCastle.Messaging.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Schedule.Models;
using StoneCastle.Security.Commons;
using StoneCastle.TrainingProgram.Models;
using System.Data.Entity;

namespace StoneCastle.Build
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext() 
        : base(Constants.ENTITY_FRAMEWORK_CONNECTION_STRING, throwIfV1Schema: false)
        {

        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<User>();

            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(x => x.PasswordHash).HasMaxLength(256);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.PhoneNumber).HasMaxLength(20);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.SecurityStamp).HasMaxLength(256);

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims").Property(x => x.ClaimType).HasMaxLength(500);
            modelBuilder.Entity<IdentityUserClaim>().Property(x => x.ClaimValue).HasMaxLength(500);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");            

            modelBuilder.Entity<User>().HasMany(u => u.RoleGroups)
                                                  .WithMany(r => r.Users)
                                                  .Map(m =>
                                                  {
                                                      m.MapLeftKey("RoleGroupId");
                                                      m.MapRightKey("UserId");
                                                      m.ToTable("UserGroups");
                                                  });

            modelBuilder.Entity<AppClaim>().HasMany(u => u.AppFunctions)
                                                  .WithMany(r => r.AppClaims)
                                                  .Map(m =>
                                                  {
                                                      m.MapLeftKey("AppClaimId");
                                                      m.MapRightKey("AppFunctionId");
                                                      m.ToTable("FunctionClaims");
                                                  });

            modelBuilder.Entity<Teacher>().HasMany(u => u.Subjects)
                                                  .WithMany(r => r.Teachers)
                                                  .Map(m =>
                                                  {
                                                      m.MapLeftKey("TeacherId");
                                                      m.MapRightKey("TrainingSubjectId");
                                                      m.ToTable("TeacherSubjects");
                                                  });

            /*modelBuilder.Entity<Teacher>().HasMany(u => u.Divisions)
                                                             .WithMany(r => r.Teachers)
                                                             .Map(m =>
                                                             {
                                                                 m.MapLeftKey("TeacherId");
                                                                 m.MapRightKey("DivisionId");
                                                                 m.ToTable("TeacherDivisions");
                                                             });*/
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Timezone> Timezone { get; set; }

        public virtual DbSet<AppClaim> AppClaim { get; set; }
        public virtual DbSet<AppFunction> AppFunction { get; set; }

        public virtual DbSet<GroupClaim> GroupClaim { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        //public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Account.Models.Account> Account { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<CRM> CRM { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        public virtual DbSet<Organization.Models.Organization> Organization { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Semester> Semester { get; set; }
        public virtual DbSet<ClassGroup> ClassGroup { get; set; }
        public virtual DbSet<ClassRoom> ClassRoom { get; set; }
        public virtual DbSet<ClassCourse> Course { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectGroup> SubjectGroup { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<TeacherDivision> TeacherDivision { get; set; }

        public virtual DbSet<TrainingProgram.Models.TrainingProgram> TrainingProgram { get; set; }
        public virtual DbSet<Course> CourseSubject { get; set; }

        public virtual DbSet<SemesterCalendar> SemesterCalendar { get; set; }
        public virtual DbSet<SemesterEvent> SemesterEvent { get; set; }
        public virtual DbSet<ClassGroupEvent> ClassGroupEvent { get; set; }
        public virtual DbSet<ClassEvent> ClassEvent { get; set; }

        public virtual DbSet<Timetable> Timetable { get; set; }
        public virtual DbSet<CourseSection> CourseSection { get; set; }
        public virtual DbSet<ClassTimetable> ClassTimetable { get; set; }

        public virtual DbSet<ScheduleStage> ScheduleStage { get; set; }       
        public virtual DbSet<ScheduleEvent> ScheduleEvent { get; set; }
        public virtual DbSet<SchedulingTable> SchedulingTable { get; set; }

        public virtual DbSet<MessagingDataMapping> MessagingDataMapping { get; set; }
        public virtual DbSet<MessagingMessage> MessagingMessage { get; set; }
        public virtual DbSet<MessagingTemplate> MessagingTemplate { get; set; }
        public virtual DbSet<MessagingTemplateContent> MessagingTemplateContent { get; set; }
        public virtual DbSet<MessagingType> MessagingType { get; set; }
    }
}
