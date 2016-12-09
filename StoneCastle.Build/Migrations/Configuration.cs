namespace StoneCastle.Build.Migrations
{
    using Application.Models;
    using Domain.Authentication.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

    internal sealed class Configuration : DbMigrationsConfiguration<StoneCastle.Build.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoneCastle.Build.AppIdentityDbContext context)
        {
            Random rand = new Random();

            #region System Admin
            List<RoleGroup> roleGroups = new List<RoleGroup>()
            {
                new RoleGroup { Id = Guid.NewGuid(), Name ="Clients"},
                new RoleGroup { Id = Guid.NewGuid(), Name ="CRMs"},
                new RoleGroup { Id = Guid.NewGuid(), Name ="Managers"},
                new RoleGroup { Id = Guid.NewGuid(), Name ="SystemAdmins"}
            };

            foreach (RoleGroup roleGroup in roleGroups)
            {
                context.RoleGroup.Add(roleGroup);
            }

            List<AppFunction> appFunctions = new List<AppFunction>()
            {
                new AppFunction { Id =  Guid.NewGuid(), Name ="Dashboard"},
                new AppFunction { Id =  Guid.NewGuid(), Name ="Reporting"},
                new AppFunction { Id =  Guid.NewGuid(), Name ="AdminPortal"},
            };

            foreach (AppFunction appFunction in appFunctions)
            {
                context.AppFunction.Add(appFunction);
            }

            List<String> claims = new List<string> { "dashboard", "usermanager", "business" };

            List<AppClaim> appClaims = new List<AppClaim>();
            for (int index = 0; index < claims.Count; index++)
            {
                AppClaim claim = new AppClaim { Id = Guid.NewGuid(), ClaimType = "role", ClaimValue = claims[index] };
                claim.RoleGroups.Add(roleGroups[3]);
                claim.AppFunctions.Add(appFunctions[index]);
                appClaims.Add(claim);
                context.AppClaim.Add(claim);
            }

            ApplicationUser admin = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "System",
                UserName = "admin@stonecastle.com",
                Email = "admin@stonecastle.com",
                IsActived = true
            };

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string password = "12345678x@X";
            IdentityResult result = manager.Create(admin, password);

            foreach (String appClaim in claims)
            {
                manager.AddClaim(admin.Id, new Claim("role", appClaim));
            }


            Account.Models.Profile adminProfile = new Account.Models.Profile()
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "System",
                Email = "admin@stonecastle.com",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsDeleted = false,
            };

            #endregion

            #region Organization
            Account.Models.Profile teacherProfile1 = new Account.Models.Profile()
            {
                Id = Guid.NewGuid(),
                FirstName = "Teacher",
                LastName = "1",
                Email = "teacher1@stonecastle.com",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsDeleted = false,
            };

            Account.Models.Profile teacherProfile2 = new Account.Models.Profile()
            {
                Id = Guid.NewGuid(),
                FirstName = "Teacher",
                LastName = "2",
                Email = "teacher2@stonecastle.com",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsDeleted = false,
            };

            Account.Models.Profile teacherProfile3 = new Account.Models.Profile()
            {
                Id = Guid.NewGuid(),
                FirstName = "Teacher",
                LastName = "3",
                Email = "teacher3@stonecastle.com",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsDeleted = false,
            };

            Account.Models.Profile teacherProfile4 = new Account.Models.Profile()
            {
                Id = Guid.NewGuid(),
                FirstName = "Teacher",
                LastName = "4",
                Email = "teacher4@stonecastle.com",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsDeleted = false,
            };

            adminProfile.User = new User()
            {
                Id = admin.Id
            };            

            context.Profile.Add(adminProfile);
            context.Profile.Add(teacherProfile1);
            context.Profile.Add(teacherProfile2);
            context.Profile.Add(teacherProfile3);
            context.Profile.Add(teacherProfile4);

            Account.Models.Account adminAccount = new Account.Models.Account()
            {
                Id = Guid.NewGuid(),
                Profile = adminProfile,
                UserType = Account.Models.USER_TYPE.ADMIN,
                ProfileType = Account.Models.PROFILE_TYPE.STAFF
            };

            Account.Models.Account teacherAccount1 = new Account.Models.Account()
            {
                Id = Guid.NewGuid(),
                Profile = teacherProfile1,
                UserType = Account.Models.USER_TYPE.CLIENT,
                ProfileType = Account.Models.PROFILE_TYPE.TEACHER
            };

            Account.Models.Account teacherAccount2 = new Account.Models.Account()
            {
                Id = Guid.NewGuid(),
                Profile = teacherProfile2,
                UserType = Account.Models.USER_TYPE.CLIENT,
                ProfileType = Account.Models.PROFILE_TYPE.TEACHER
            };

            Account.Models.Account teacherAccount3 = new Account.Models.Account()
            {
                Id = Guid.NewGuid(),
                Profile = teacherProfile3,
                UserType = Account.Models.USER_TYPE.CLIENT,
                ProfileType = Account.Models.PROFILE_TYPE.TEACHER
            };

            Account.Models.Account teacherAccount4 = new Account.Models.Account()
            {
                Id = Guid.NewGuid(),
                Profile = teacherProfile4,
                UserType = Account.Models.USER_TYPE.CLIENT,
                ProfileType = Account.Models.PROFILE_TYPE.TEACHER
            };

            context.Account.Add(adminAccount);
            context.Account.Add(teacherAccount1);
            context.Account.Add(teacherAccount2);
            context.Account.Add(teacherAccount3);
            context.Account.Add(teacherAccount4);

            Account.Models.Teacher teacher1 = new Account.Models.Teacher()
            {
                Id = Guid.NewGuid(),
                Account = teacherAccount1,
                IsActive = true,
                IsDeleted = false
            };

            Account.Models.Teacher teacher2 = new Account.Models.Teacher()
            {
                Id = Guid.NewGuid(),
                Account = teacherAccount2,
                IsActive = true,
                IsDeleted = false
            };

            Account.Models.Teacher teacher3 = new Account.Models.Teacher()
            {
                Id = Guid.NewGuid(),
                Account = teacherAccount3,
                IsActive = true,
                IsDeleted = false
            };

            Account.Models.Teacher teacher4 = new Account.Models.Teacher()
            {
                Id = Guid.NewGuid(),
                Account = teacherAccount4,
                IsActive = true,
                IsDeleted = false
            };

            context.Teacher.Add(teacher1);
            context.Teacher.Add(teacher2);
            context.Teacher.Add(teacher3);
            context.Teacher.Add(teacher4);

            Organization.Models.Organization organization = new Organization.Models.Organization()
            {
                Id = Guid.NewGuid(),
                Name = "SHOOL APP",
                ShortName = "SHOOL APP",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };

            context.Organization.Add(organization);

            Organization.Models.Semester semester2016 = new Organization.Models.Semester()
            {
                Id = Guid.NewGuid(),
                Name = "2016-2017",
                Organization = organization,                
                IsActive = true,
                IsDeleted = false                
            };

            context.Semester.Add(semester2016);

            Organization.Models.Building building1 = new Organization.Models.Building()
            {
                Id = Guid.NewGuid(),
                Semester = semester2016,
                Name = "Building 1",
                Code = "Building 1",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.Building.Add(building1);

            Organization.Models.Room room1 = new Organization.Models.Room()
            {
                Id = Guid.NewGuid(),
                Building = building1,
                Name = "Room 1",
                Code = "Room 1",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.Room.Add(room1);

            Organization.Models.Room room2 = new Organization.Models.Room()
            {
                Id = Guid.NewGuid(),
                Building = building1,
                Name = "Room 2",
                Code = "Room 2",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.Room.Add(room2);

            Organization.Models.Room room3 = new Organization.Models.Room()
            {
                Id = Guid.NewGuid(),
                Building = building1,
                Name = "Room 3",
                Code = "Room 3",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.Room.Add(room3);

            Organization.Models.Room room4 = new Organization.Models.Room()
            {
                Id = Guid.NewGuid(),
                Building = building1,
                Name = "Room 4",
                Code = "Room 4",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.Room.Add(room4);

            Organization.Models.Room room5 = new Organization.Models.Room()
            {
                Id = Guid.NewGuid(),
                Building = building1,
                Name = "Room 5",
                Code = "Room 5",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.Room.Add(room5);


            Organization.Models.Division division1 = new Organization.Models.Division()
            {
                Id = Guid.NewGuid(),
                Name = "Math",
                Semester = semester2016,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.TeacherDivision teacherDivision1 = new Organization.Models.TeacherDivision()
            {
                Id = Guid.NewGuid(),
                Teacher = teacher1,
                Division = division1,
            };

            Organization.Models.TeacherDivision teacherDivision2 = new Organization.Models.TeacherDivision()
            {
                Id = Guid.NewGuid(),
                Teacher = teacher2,
                Division = division1,
            };

            Organization.Models.TeacherDivision teacherDivision3 = new Organization.Models.TeacherDivision()
            {
                Id = Guid.NewGuid(),
                Teacher = teacher3,
                Division = division1,
            };

            Organization.Models.TeacherDivision teacherDivision4 = new Organization.Models.TeacherDivision()
            {
                Id = Guid.NewGuid(),
                Teacher = teacher4,
                Division = division1,
            };


            context.Division.Add(division1);
            context.TeacherDivision.Add(teacherDivision1);
            context.TeacherDivision.Add(teacherDivision2);
            context.TeacherDivision.Add(teacherDivision3);
            context.TeacherDivision.Add(teacherDivision4);


            TrainingProgram.Models.TrainingProgram program10 = new TrainingProgram.Models.TrainingProgram()
            {
                Id = Guid.NewGuid(),
                Semester = semester2016,
                Name = "Program 10",
                IsActive = true,
                IsDeleted = false
            };

            TrainingProgram.Models.TrainingProgram program11 = new TrainingProgram.Models.TrainingProgram()
            {
                Id = Guid.NewGuid(),
                Semester = semester2016,
                Name = "Program 11",
                IsActive = true,
                IsDeleted = false
            };

            context.TrainingProgram.Add(program10);
            context.TrainingProgram.Add(program11);

            Organization.Models.ClassGroup classGroup10 = new Organization.Models.ClassGroup()
            {
                Id = Guid.NewGuid(),
                Name = "10",
                Semester = semester2016,
                TrainingProgram = program10,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };


            context.ClassGroup.Add(classGroup10);

            Organization.Models.ClassRoom class10A = new Organization.Models.ClassRoom()
            {
                Id = Guid.NewGuid(),
                Name = "10A",
                ClassGroup = classGroup10,
                HomeroomTeacher = teacher1,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassRoom class10B = new Organization.Models.ClassRoom()
            {
                Id = Guid.NewGuid(),
                Name = "10B",
                ClassGroup = classGroup10,
                HomeroomTeacher = teacher2,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassRoom class10C = new Organization.Models.ClassRoom()
            {
                Id = Guid.NewGuid(),
                Name = "10C",
                ClassGroup = classGroup10,
                HomeroomTeacher = teacher3,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.ClassRoom.Add(class10A);
            context.ClassRoom.Add(class10B);
            context.ClassRoom.Add(class10C);

            Organization.Models.ClassGroup classGroup11 = new Organization.Models.ClassGroup()
            {
                Id = Guid.NewGuid(),
                Name = "11",
                Semester = semester2016,
                TrainingProgram = program11,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };


            context.ClassGroup.Add(classGroup11);

            Organization.Models.ClassRoom class11A = new Organization.Models.ClassRoom()
            {
                Id = Guid.NewGuid(),
                Name = "11A",
                ClassGroup = classGroup11,
                HomeroomTeacher = teacher4,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassRoom class11B = new Organization.Models.ClassRoom()
            {
                Id = Guid.NewGuid(),
                Name = "11B",
                ClassGroup = classGroup11,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.ClassRoom.Add(class11A);
            context.ClassRoom.Add(class11B);

            Organization.Models.SubjectGroup subjectGroup = new Organization.Models.SubjectGroup()
            {
                Id = Guid.NewGuid(),
                Semester = semester2016,
                Name = "Natural",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.SubjectGroup.Add(subjectGroup);

            Organization.Models.Subject math10 = new Organization.Models.Subject()
            {
                Id = Guid.NewGuid(),
                Name = "Math 10",
                SubjectGroup = subjectGroup,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };

            Organization.Models.Subject geo10 = new Organization.Models.Subject()
            {
                Id = Guid.NewGuid(),
                Name = "GEO 10",
                SubjectGroup = subjectGroup,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };

            Organization.Models.Subject history10 = new Organization.Models.Subject()
            {
                Id = Guid.NewGuid(),
                Name = "History 10",
                SubjectGroup = subjectGroup,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };

            Organization.Models.Subject math11 = new Organization.Models.Subject()
            {
                Id = Guid.NewGuid(),
                Name = "Math 11",
                SubjectGroup = subjectGroup,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };

            Organization.Models.Subject geo11 = new Organization.Models.Subject()
            {
                Id = Guid.NewGuid(),
                Name = "GEO 11",
                SubjectGroup = subjectGroup,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };

            Organization.Models.Subject history11 = new Organization.Models.Subject()
            {
                Id = Guid.NewGuid(),
                Name = "History 11",
                SubjectGroup = subjectGroup,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false,
            };


            context.Subject.Add(math10);
            context.Subject.Add(geo10);
            context.Subject.Add(history10);

            context.Subject.Add(math11);
            context.Subject.Add(geo11);
            context.Subject.Add(history11);

            teacher1.Subjects.Add(math10);
            teacher1.Subjects.Add(math11);

            teacher2.Subjects.Add(geo10);
            teacher2.Subjects.Add(geo11);

            teacher3.Subjects.Add(history10);
            teacher3.Subjects.Add(history11);

            teacher4.Subjects.Add(history11);

            TrainingProgram.Models.Course course10Math10 = new TrainingProgram.Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgram = program10,
                Subject = math10,
                SectionPerWeek = 3,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            TrainingProgram.Models.Course course10Geo10 = new TrainingProgram.Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgram = program10,
                Subject = geo10,
                SectionPerWeek = 3,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            TrainingProgram.Models.Course course10History10 = new TrainingProgram.Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgram = program10,
                Subject = history10,
                SectionPerWeek = 3,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.CourseSubject.Add(course10Math10);
            context.CourseSubject.Add(course10Geo10);
            context.CourseSubject.Add(course10History10);


            TrainingProgram.Models.Course course11Math11 = new TrainingProgram.Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgram = program11,
                Subject = math11,
                SectionPerWeek = 2,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            TrainingProgram.Models.Course course11Geo11 = new TrainingProgram.Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgram = program11,
                Subject = geo11,
                SectionPerWeek = 3,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            TrainingProgram.Models.Course course11History11 = new TrainingProgram.Models.Course()
            {
                Id = Guid.NewGuid(),
                TrainingProgram = program11,
                Subject = history11,
                SectionPerWeek = 1,
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                IsActive = true,
                IsDeleted = false
            };

            context.CourseSubject.Add(course11Math11);
            context.CourseSubject.Add(course11Geo11);
            context.CourseSubject.Add(course11History11);


            Organization.Models.ClassCourse courseTeacher1MathClass10A = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10A,
                Teacher = teacher1,
                Course = course10Math10,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher1MathClass10B = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10B,
                Teacher = teacher1,
                Course = course10Math10,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher1MathClass10C = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10C,
                Teacher = teacher1,
                Course = course10Math10,
                IsActive = true,
                IsDeleted = false
            };

            context.Course.Add(courseTeacher1MathClass10A);
            context.Course.Add(courseTeacher1MathClass10B);
            context.Course.Add(courseTeacher1MathClass10C);

            Organization.Models.ClassCourse courseTeacher2GeoClass10A = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10A,
                Teacher = teacher2,
                Course = course10Geo10,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher2GeoClass10B = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10B,
                Teacher = teacher2,
                Course = course10Geo10,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher2GeoClass10C = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10C,
                Teacher = teacher2,
                Course = course10Geo10,
                IsActive = true,
                IsDeleted = false
            };

            context.Course.Add(courseTeacher2GeoClass10A);
            context.Course.Add(courseTeacher2GeoClass10B);
            context.Course.Add(courseTeacher2GeoClass10C);

            Organization.Models.ClassCourse courseTeacher3HistoryClass10A = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10A,
                Teacher = teacher3,
                Course = course10History10,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher3HistoryClass10B = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10B,
                Teacher = teacher3,
                Course = course10History10,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher3HistoryClass10C = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class10C,
                Teacher = teacher3,
                Course = course10History10,
                IsActive = true,
                IsDeleted = false
            };

            context.Course.Add(courseTeacher3HistoryClass10A);
            context.Course.Add(courseTeacher3HistoryClass10B);
            context.Course.Add(courseTeacher3HistoryClass10C);

            Organization.Models.ClassCourse courseTeacher1MathClass11A = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class11A,
                Teacher = teacher1,
                Course = course11Math11,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher1MathClass11B = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class11B,
                Teacher = teacher1,
                Course = course11Math11,
                IsActive = true,
                IsDeleted = false
            };

            context.Course.Add(courseTeacher1MathClass11A);
            context.Course.Add(courseTeacher1MathClass11B);

            Organization.Models.ClassCourse courseTeacher2GeoClass11A = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class11A,
                Teacher = teacher2,
                Course = course11Geo11,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher2GeoClass11B = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class11B,
                Teacher = teacher2,
                Course = course11Geo11,
                IsActive = true,
                IsDeleted = false
            };

            context.Course.Add(courseTeacher2GeoClass11A);
            context.Course.Add(courseTeacher2GeoClass11B);

            Organization.Models.ClassCourse courseTeacher4HistoryClass11A = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class11A,
                Teacher = teacher4,
                Course = course11History11,
                IsActive = true,
                IsDeleted = false
            };

            Organization.Models.ClassCourse courseTeacher4HistoryClass11B = new Organization.Models.ClassCourse()
            {
                Id = Guid.NewGuid(),
                ClassRoom = class11B,
                Teacher = teacher4,
                Course = course11History11,
                IsActive = true,
                IsDeleted = false
            };

            context.Course.Add(courseTeacher4HistoryClass11A);
            context.Course.Add(courseTeacher4HistoryClass11B);

            Schedule.Models.Timetable timetable1 = new Schedule.Models.Timetable()
            {
                Id = Guid.NewGuid(),
            };

            Schedule.Models.CourseSection courseSection1 = new Schedule.Models.CourseSection()
            {
                Id = Guid.NewGuid(),
                Day = DayOfWeek.Tuesday,
                Shift = Schedule.Models.SHIFT.MORNING,
                Timetable = timetable1
            };

            Schedule.Models.CourseSection courseSection2 = new Schedule.Models.CourseSection()
            {
                Id = Guid.NewGuid(),
                Day = DayOfWeek.Tuesday,
                Shift = Schedule.Models.SHIFT.AFTERNOON,
                Timetable = timetable1
            };

            Schedule.Models.CourseSection courseSection3 = new Schedule.Models.CourseSection()
            {
                Id = Guid.NewGuid(),
                Day = DayOfWeek.Thursday,
                Shift = Schedule.Models.SHIFT.AFTERNOON,
                Timetable = timetable1
            };


            context.CourseSection.Add(courseSection1);
            context.CourseSection.Add(courseSection2);
            context.CourseSection.Add(courseSection3);
            context.Timetable.Add(timetable1);

            context.SaveChanges();
            #endregion

            #region Email Template
            Messaging.Models.MessagingType msgSystem = new Messaging.Models.MessagingType()
            {
                Id = 301,
                MessagingTypeTitle = "System",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand)
            };

            Messaging.Models.MessagingType msgTechnicalSupport = new Messaging.Models.MessagingType()
            {
                Id = 302,
                MessagingTypeTitle = "Technical Support",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand)
            };

            Messaging.Models.MessagingType msgNotification = new Messaging.Models.MessagingType()
            {
                Id = 501,
                MessagingTypeTitle = "Notification",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand)
            };


            Messaging.Models.MessagingType msgCustomerService = new Messaging.Models.MessagingType()
            {
                Id = 502,
                MessagingTypeTitle = "Customer Service",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand)
            };

            context.MessagingType.Add(msgSystem);
            context.MessagingType.Add(msgTechnicalSupport);
            context.MessagingType.Add(msgNotification);
            context.MessagingType.Add(msgCustomerService);

            Messaging.Models.MessagingTemplate etSystem = new Messaging.Models.MessagingTemplate()
            {
                Id = Guid.NewGuid(),
                MessagingType = msgSystem,
                MessagingTemplateName = "System",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            Messaging.Models.MessagingTemplate etTechSupport = new Messaging.Models.MessagingTemplate()
            {
                Id = Guid.NewGuid(),
                MessagingType = msgTechnicalSupport,
                MessagingTemplateName = "Tech Support",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            Messaging.Models.MessagingTemplate etNotification = new Messaging.Models.MessagingTemplate()
            {
                Id = Guid.NewGuid(),
                MessagingType = msgNotification,
                MessagingTemplateName = "Notification",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            Messaging.Models.MessagingTemplate etCustomerService = new Messaging.Models.MessagingTemplate()
            {
                Id = Guid.NewGuid(),
                MessagingType = msgCustomerService,
                MessagingTemplateName = "Customer Service",
                HighlightColor = Commons.Ultility.GetHighlightColor(rand),
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            context.MessagingTemplate.Add(etSystem);
            context.MessagingTemplate.Add(etTechSupport);
            context.MessagingTemplate.Add(etNotification);
            context.MessagingTemplate.Add(etCustomerService);          

            Messaging.Models.MessagingTemplateContent etcSystem = new Messaging.Models.MessagingTemplateContent()
            {
                Id = Guid.NewGuid(),
                MessagingTemplate = etSystem,
                MessagingFromName = "Octolium.com",
                MessagingFromEmailAddress = "system@octolium.com",
                MessagingCc = "octolium@gmail.com",
                MessagingTo = "{{emailAddress}}",
                MessagingSubject = "[Octolium.com] System",
                MessagingContent = "This is message from Octolium.com",
                Lang = "en",
                FromDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            Messaging.Models.MessagingTemplateContent etcTechSupport = new Messaging.Models.MessagingTemplateContent()
            {
                Id = Guid.NewGuid(),
                MessagingTemplate = etTechSupport,
                MessagingFromName = "Octolium.com",
                MessagingFromEmailAddress = "system@octolium.com",
                MessagingCc = "octolium@gmail.com",
                MessagingTo = "{{emailAddress}}",
                MessagingSubject = "[Octolium.com] Tech Support",
                MessagingContent = "This is message from Octolium.com",
                Lang = "en",
                FromDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            Messaging.Models.MessagingTemplateContent etcNotification = new Messaging.Models.MessagingTemplateContent()
            {
                Id = Guid.NewGuid(),
                MessagingTemplate = etNotification,
                MessagingFromName = "Octolium.com",
                MessagingFromEmailAddress = "system@octolium.com",
                MessagingCc = "octolium@gmail.com",
                MessagingTo = "{{emailAddress}}",
                MessagingSubject = "[Octolium.com] Notification",
                MessagingContent = "This is message from Octolium.com",
                Lang = "en",
                FromDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            Messaging.Models.MessagingTemplateContent etcCustomerService = new Messaging.Models.MessagingTemplateContent()
            {
                Id = Guid.NewGuid(),
                MessagingTemplate = etCustomerService,
                MessagingFromName = "Octolium.com",
                MessagingFromEmailAddress = "system@octolium.com",
                MessagingCc = "octolium@gmail.com",
                MessagingTo = "{{emailAddress}}",
                MessagingSubject = "[Octolium.com] Customer Service",
                MessagingContent = "This is message from Octolium.com",
                Lang = "en",
                FromDate = DateTime.UtcNow,              
                CreatedDate = DateTime.UtcNow,
                IsPublish = true
            };

            context.MessagingTemplateContent.Add(etcSystem);
            context.MessagingTemplateContent.Add(etcNotification);
            context.MessagingTemplateContent.Add(etcTechSupport);
            context.MessagingTemplateContent.Add(etcCustomerService);
            #endregion
        }
    }
}
