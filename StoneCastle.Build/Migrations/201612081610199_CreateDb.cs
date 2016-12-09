namespace StoneCastle.Build.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProfileId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserType = c.Int(nullable: false),
                        ProfileType = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Boolean(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        StartDate = c.DateTime(),
                        DOB = c.DateTime(),
                        Lang = c.String(),
                        CountryCode = c.String(),
                        TimezoneCode = c.String(),
                        AvatarPhoto = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        UserId = c.String(),
                        UserType = c.Int(nullable: false),
                        ProfileType = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppClaims",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClaimType = c.String(maxLength: 500),
                        ClaimValue = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppFunctions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 256),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        AppClaim_Id = c.Guid(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppClaims", t => t.AppClaim_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.AppClaim_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.GroupClaims",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClaimType = c.String(maxLength: 256),
                        ClaimValue = c.String(maxLength: 500),
                        RoleGroupId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleGroups", t => t.RoleGroupId, cascadeDelete: true)
                .Index(t => t.RoleGroupId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        SemesterId = c.Guid(nullable: false),
                        HighlightColor = c.String(maxLength: 32),
                        LogoUrl = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        ShortName = c.String(maxLength: 500),
                        OrganizationId = c.Guid(nullable: false),
                        HighlightColor = c.String(maxLength: 32),
                        TimetableId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.OrganizationId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        ShortName = c.String(maxLength: 500),
                        LogoUrl = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        ShiftPerDay = c.Int(nullable: false),
                        SlotPerShift = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        HighlightColor = c.String(maxLength: 32),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseSections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Day = c.Int(nullable: false),
                        Shift = c.Int(nullable: false),
                        Slot = c.Short(nullable: false),
                        Stage = c.Int(nullable: false),
                        TimetableId = c.Guid(nullable: false),
                        ClassCourseId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassCourses", t => t.ClassCourseId)
                .ForeignKey("dbo.Timetables", t => t.TimetableId, cascadeDelete: true)
                .Index(t => t.TimetableId)
                .Index(t => t.ClassCourseId);
            
            CreateTable(
                "dbo.ClassCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeacherId = c.Guid(),
                        ClassRoomId = c.Guid(),
                        CourseId = c.Guid(),
                        RoomId = c.Guid(),
                        TimetableId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassRooms", t => t.ClassRoomId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.TeacherId)
                .Index(t => t.ClassRoomId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.ClassRooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        LogoUrl = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        ClassGroupId = c.Guid(),
                        TeacherId = c.Guid(),
                        RoomId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassGroups", t => t.ClassGroupId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.ClassGroupId)
                .Index(t => t.TeacherId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.ClassGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        LogoUrl = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        IsActive = c.Boolean(nullable: false),
                        SemesterId = c.Guid(nullable: false),
                        TrainingProgramId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.TrainingPrograms", t => t.TrainingProgramId)
                .Index(t => t.SemesterId)
                .Index(t => t.TrainingProgramId);
            
            CreateTable(
                "dbo.TrainingPrograms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        SemesterId = c.Guid(nullable: false),
                        HighlightColor = c.String(maxLength: 32),
                        LogoUrl = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        TimetableId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.SemesterId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TrainingProgramId = c.Guid(nullable: false),
                        SubjectId = c.Guid(),
                        TimetableId = c.Guid(),
                        TotalSection = c.Int(nullable: false),
                        SectionPerWeek = c.Int(nullable: false),
                        IsTeachingByHomeroomTeacher = c.Boolean(nullable: false),
                        HighlightColor = c.String(maxLength: 32),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .ForeignKey("dbo.TrainingPrograms", t => t.TrainingProgramId, cascadeDelete: true)
                .Index(t => t.TrainingProgramId)
                .Index(t => t.SubjectId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        HighlightColor = c.String(maxLength: 32),
                        SubjectGroupId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectGroups", t => t.SubjectGroupId, cascadeDelete: true)
                .Index(t => t.SubjectGroupId);
            
            CreateTable(
                "dbo.SubjectGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        SemesterId = c.Guid(nullable: false),
                        HighlightColor = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.TeacherDivisions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimetableId = c.Guid(),
                        DivisionId = c.Guid(nullable: false),
                        TeacherId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.TimetableId)
                .Index(t => t.DivisionId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        HighlightColor = c.String(maxLength: 32),
                        LogoUrl = c.String(maxLength: 255),
                        SemesterId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 32),
                        LogoUrl = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        BuildingId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.ClassEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        ClassRoomId = c.Guid(nullable: false),
                        ClassGroupEventId = c.Guid(nullable: false),
                        TimetableId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassGroupEvents", t => t.ClassGroupEventId, cascadeDelete: true)
                .ForeignKey("dbo.ClassRooms", t => t.ClassRoomId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.ClassRoomId)
                .Index(t => t.ClassGroupEventId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.ClassGroupEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        SemesterEventId = c.Guid(nullable: false),
                        ClassGroupId = c.Guid(nullable: false),
                        TimetableId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassGroups", t => t.ClassGroupId, cascadeDelete: true)
                .ForeignKey("dbo.SemesterEvents", t => t.SemesterEventId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.SemesterEventId)
                .Index(t => t.ClassGroupId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.SemesterEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        SemesterCalendarId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SemesterCalendars", t => t.SemesterCalendarId, cascadeDelete: true)
                .Index(t => t.SemesterCalendarId);
            
            CreateTable(
                "dbo.SemesterCalendars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        TimetableId = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Timetables", t => t.TimetableId)
                .Index(t => t.TimetableId);
            
            CreateTable(
                "dbo.ClassTimetables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClassRoomId = c.Guid(nullable: false),
                        TimetableId = c.Guid(nullable: false),
                        SchedulingTableId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassRooms", t => t.ClassRoomId, cascadeDelete: true)
                .ForeignKey("dbo.SchedulingTables", t => t.SchedulingTableId, cascadeDelete: true)
                .ForeignKey("dbo.Timetables", t => t.TimetableId, cascadeDelete: true)
                .Index(t => t.ClassRoomId)
                .Index(t => t.TimetableId)
                .Index(t => t.SchedulingTableId);
            
            CreateTable(
                "dbo.SchedulingTables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        ShortName = c.String(maxLength: 500),
                        LogoUrl = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        Stage = c.Int(nullable: false),
                        SemesterId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        AddedStamp = c.DateTime(nullable: false),
                        AddedUserId = c.Guid(),
                        ChangedStamp = c.DateTime(),
                        ChangedUserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.ScheduleEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Stage = c.Int(nullable: false),
                        SchedulingTableId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchedulingTables", t => t.SchedulingTableId, cascadeDelete: true)
                .Index(t => t.SchedulingTableId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(maxLength: 2),
                        CountryName = c.String(maxLength: 128),
                        CountryNumCode = c.Int(),
                        CountryPhoneCode = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CRMs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManagerId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Managers", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessagingDataMapping",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MappingName = c.String(),
                        TokenKey = c.String(),
                        TableName = c.String(maxLength: 128),
                        ColumnName = c.String(maxLength: 128),
                        RequiredColumnName = c.String(maxLength: 128),
                        Format = c.String(),
                        SqlQuery = c.String(),
                        Value = c.String(),
                        IsPublish = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessagingMessages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MessagingTemplateContentId = c.Guid(nullable: false),
                        EmailDeliveryProvider = c.String(),
                        MessagingSubject = c.String(),
                        MessagingFromName = c.String(),
                        MessagingFromEmailAddress = c.String(),
                        MessagingTo = c.String(),
                        MessagingCc = c.String(),
                        MessagingBcc = c.String(),
                        MessagingContent = c.String(),
                        Tags = c.String(),
                        IsSent = c.Boolean(nullable: false),
                        IsMarkedAsRead = c.Boolean(nullable: false),
                        SentDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessagingTemplateContents", t => t.MessagingTemplateContentId, cascadeDelete: true)
                .Index(t => t.MessagingTemplateContentId);
            
            CreateTable(
                "dbo.MessagingTemplateContents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MessagingTemplateId = c.Guid(nullable: false),
                        Lang = c.String(),
                        MessagingSubject = c.String(),
                        MessagingFromName = c.String(),
                        MessagingFromEmailAddress = c.String(),
                        MessagingTo = c.String(),
                        MessagingCc = c.String(),
                        MessagingBcc = c.String(),
                        MessagingContent = c.String(),
                        Tags = c.String(),
                        IsPublish = c.Boolean(nullable: false),
                        FromDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessagingTemplates", t => t.MessagingTemplateId, cascadeDelete: true)
                .Index(t => t.MessagingTemplateId);
            
            CreateTable(
                "dbo.MessagingTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MessagingTypeId = c.Int(nullable: false),
                        MessagingTemplateName = c.String(maxLength: 255),
                        HighlightColor = c.String(maxLength: 32),
                        IsPublish = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessagingTypes", t => t.MessagingTypeId, cascadeDelete: true)
                .Index(t => t.MessagingTypeId);
            
            CreateTable(
                "dbo.MessagingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessagingTypeTitle = c.String(maxLength: 32),
                        HighlightColor = c.String(maxLength: 32),
                        IsEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ScheduleStages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timezones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(maxLength: 2),
                        Coordinates = c.String(maxLength: 15),
                        TimezoneName = c.String(maxLength: 35),
                        UtcOffset = c.Single(),
                        UtcDstOffset = c.Single(),
                        RawOffset = c.Single(),
                        IsDefault = c.Boolean(nullable: false),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        ChangePassword = c.Boolean(nullable: false),
                        IsActived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 256),
                        SecurityStamp = c.String(maxLength: 256),
                        PhoneNumber = c.String(maxLength: 20),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(maxLength: 500),
                        ClaimValue = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FunctionClaims",
                c => new
                    {
                        AppClaimId = c.Guid(nullable: false),
                        AppFunctionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppClaimId, t.AppFunctionId })
                .ForeignKey("dbo.AppClaims", t => t.AppClaimId, cascadeDelete: true)
                .ForeignKey("dbo.AppFunctions", t => t.AppFunctionId, cascadeDelete: true)
                .Index(t => t.AppClaimId)
                .Index(t => t.AppFunctionId);
            
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        TeacherId = c.Guid(nullable: false),
                        TrainingSubjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherId, t.TrainingSubjectId })
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.TrainingSubjectId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.TrainingSubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleGroups", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MessagingMessages", "MessagingTemplateContentId", "dbo.MessagingTemplateContents");
            DropForeignKey("dbo.MessagingTemplateContents", "MessagingTemplateId", "dbo.MessagingTemplates");
            DropForeignKey("dbo.MessagingTemplates", "MessagingTypeId", "dbo.MessagingTypes");
            DropForeignKey("dbo.CRMs", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Clients", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.ClassTimetables", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.ClassTimetables", "SchedulingTableId", "dbo.SchedulingTables");
            DropForeignKey("dbo.SchedulingTables", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.ScheduleEvents", "SchedulingTableId", "dbo.SchedulingTables");
            DropForeignKey("dbo.ClassTimetables", "ClassRoomId", "dbo.ClassRooms");
            DropForeignKey("dbo.ClassEvents", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.ClassEvents", "ClassRoomId", "dbo.ClassRooms");
            DropForeignKey("dbo.ClassEvents", "ClassGroupEventId", "dbo.ClassGroupEvents");
            DropForeignKey("dbo.ClassGroupEvents", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.ClassGroupEvents", "SemesterEventId", "dbo.SemesterEvents");
            DropForeignKey("dbo.SemesterEvents", "SemesterCalendarId", "dbo.SemesterCalendars");
            DropForeignKey("dbo.SemesterCalendars", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.ClassGroupEvents", "ClassGroupId", "dbo.ClassGroups");
            DropForeignKey("dbo.Buildings", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Semesters", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.CourseSections", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.CourseSections", "ClassCourseId", "dbo.ClassCourses");
            DropForeignKey("dbo.ClassCourses", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.ClassCourses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.ClassCourses", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassRooms", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.ClassRooms", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.ClassCourses", "ClassRoomId", "dbo.ClassRooms");
            DropForeignKey("dbo.ClassGroups", "TrainingProgramId", "dbo.TrainingPrograms");
            DropForeignKey("dbo.TrainingPrograms", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.TrainingPrograms", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "TrainingProgramId", "dbo.TrainingPrograms");
            DropForeignKey("dbo.Courses", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.Courses", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.TeacherDivisions", "TimetableId", "dbo.Timetables");
            DropForeignKey("dbo.TeacherDivisions", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherDivisions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.TeacherSubjects", "TrainingSubjectId", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Subjects", "SubjectGroupId", "dbo.SubjectGroups");
            DropForeignKey("dbo.SubjectGroups", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.ClassGroups", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.ClassRooms", "ClassGroupId", "dbo.ClassGroups");
            DropForeignKey("dbo.Semesters", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.RoleGroups", "AppClaim_Id", "dbo.AppClaims");
            DropForeignKey("dbo.GroupClaims", "RoleGroupId", "dbo.RoleGroups");
            DropForeignKey("dbo.FunctionClaims", "AppFunctionId", "dbo.AppFunctions");
            DropForeignKey("dbo.FunctionClaims", "AppClaimId", "dbo.AppClaims");
            DropForeignKey("dbo.Accounts", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.TeacherSubjects", new[] { "TrainingSubjectId" });
            DropIndex("dbo.TeacherSubjects", new[] { "TeacherId" });
            DropIndex("dbo.FunctionClaims", new[] { "AppFunctionId" });
            DropIndex("dbo.FunctionClaims", new[] { "AppClaimId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.MessagingTemplates", new[] { "MessagingTypeId" });
            DropIndex("dbo.MessagingTemplateContents", new[] { "MessagingTemplateId" });
            DropIndex("dbo.MessagingMessages", new[] { "MessagingTemplateContentId" });
            DropIndex("dbo.CRMs", new[] { "ManagerId" });
            DropIndex("dbo.Clients", new[] { "AccountId" });
            DropIndex("dbo.ScheduleEvents", new[] { "SchedulingTableId" });
            DropIndex("dbo.SchedulingTables", new[] { "SemesterId" });
            DropIndex("dbo.ClassTimetables", new[] { "SchedulingTableId" });
            DropIndex("dbo.ClassTimetables", new[] { "TimetableId" });
            DropIndex("dbo.ClassTimetables", new[] { "ClassRoomId" });
            DropIndex("dbo.SemesterCalendars", new[] { "TimetableId" });
            DropIndex("dbo.SemesterEvents", new[] { "SemesterCalendarId" });
            DropIndex("dbo.ClassGroupEvents", new[] { "TimetableId" });
            DropIndex("dbo.ClassGroupEvents", new[] { "ClassGroupId" });
            DropIndex("dbo.ClassGroupEvents", new[] { "SemesterEventId" });
            DropIndex("dbo.ClassEvents", new[] { "TimetableId" });
            DropIndex("dbo.ClassEvents", new[] { "ClassGroupEventId" });
            DropIndex("dbo.ClassEvents", new[] { "ClassRoomId" });
            DropIndex("dbo.Rooms", new[] { "BuildingId" });
            DropIndex("dbo.Divisions", new[] { "SemesterId" });
            DropIndex("dbo.TeacherDivisions", new[] { "TeacherId" });
            DropIndex("dbo.TeacherDivisions", new[] { "DivisionId" });
            DropIndex("dbo.TeacherDivisions", new[] { "TimetableId" });
            DropIndex("dbo.Teachers", new[] { "AccountId" });
            DropIndex("dbo.SubjectGroups", new[] { "SemesterId" });
            DropIndex("dbo.Subjects", new[] { "SubjectGroupId" });
            DropIndex("dbo.Courses", new[] { "TimetableId" });
            DropIndex("dbo.Courses", new[] { "SubjectId" });
            DropIndex("dbo.Courses", new[] { "TrainingProgramId" });
            DropIndex("dbo.TrainingPrograms", new[] { "TimetableId" });
            DropIndex("dbo.TrainingPrograms", new[] { "SemesterId" });
            DropIndex("dbo.ClassGroups", new[] { "TrainingProgramId" });
            DropIndex("dbo.ClassGroups", new[] { "SemesterId" });
            DropIndex("dbo.ClassRooms", new[] { "RoomId" });
            DropIndex("dbo.ClassRooms", new[] { "TeacherId" });
            DropIndex("dbo.ClassRooms", new[] { "ClassGroupId" });
            DropIndex("dbo.ClassCourses", new[] { "TimetableId" });
            DropIndex("dbo.ClassCourses", new[] { "RoomId" });
            DropIndex("dbo.ClassCourses", new[] { "CourseId" });
            DropIndex("dbo.ClassCourses", new[] { "ClassRoomId" });
            DropIndex("dbo.ClassCourses", new[] { "TeacherId" });
            DropIndex("dbo.CourseSections", new[] { "ClassCourseId" });
            DropIndex("dbo.CourseSections", new[] { "TimetableId" });
            DropIndex("dbo.Semesters", new[] { "TimetableId" });
            DropIndex("dbo.Semesters", new[] { "OrganizationId" });
            DropIndex("dbo.Buildings", new[] { "SemesterId" });
            DropIndex("dbo.GroupClaims", new[] { "RoleGroupId" });
            DropIndex("dbo.RoleGroups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RoleGroups", new[] { "AppClaim_Id" });
            DropIndex("dbo.Accounts", new[] { "ProfileId" });
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.FunctionClaims");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Timezones");
            DropTable("dbo.ScheduleStages");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.MessagingTypes");
            DropTable("dbo.MessagingTemplates");
            DropTable("dbo.MessagingTemplateContents");
            DropTable("dbo.MessagingMessages");
            DropTable("dbo.MessagingDataMapping");
            DropTable("dbo.Managers");
            DropTable("dbo.CRMs");
            DropTable("dbo.Countries");
            DropTable("dbo.Clients");
            DropTable("dbo.ScheduleEvents");
            DropTable("dbo.SchedulingTables");
            DropTable("dbo.ClassTimetables");
            DropTable("dbo.SemesterCalendars");
            DropTable("dbo.SemesterEvents");
            DropTable("dbo.ClassGroupEvents");
            DropTable("dbo.ClassEvents");
            DropTable("dbo.Rooms");
            DropTable("dbo.Divisions");
            DropTable("dbo.TeacherDivisions");
            DropTable("dbo.Teachers");
            DropTable("dbo.SubjectGroups");
            DropTable("dbo.Subjects");
            DropTable("dbo.Courses");
            DropTable("dbo.TrainingPrograms");
            DropTable("dbo.ClassGroups");
            DropTable("dbo.ClassRooms");
            DropTable("dbo.ClassCourses");
            DropTable("dbo.CourseSections");
            DropTable("dbo.Timetables");
            DropTable("dbo.Organizations");
            DropTable("dbo.Semesters");
            DropTable("dbo.Buildings");
            DropTable("dbo.GroupClaims");
            DropTable("dbo.RoleGroups");
            DropTable("dbo.AppFunctions");
            DropTable("dbo.AppClaims");
            DropTable("dbo.Profiles");
            DropTable("dbo.Accounts");
        }
    }
}
