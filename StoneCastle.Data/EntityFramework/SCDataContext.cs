using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using StoneCastle.Application.Models;
using StoneCastle.Domain.Models;
using StoneCastle.Account.Models;
using StoneCastle.Organization.Models;
using StoneCastle.TrainingProgram.Models;
using StoneCastle.Schedule.Models;
using System.Linq.Expressions;
using StoneCastle.Domain;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Data.EntityFramework
{
    public class SCDataContext : DbContext, ISCDataContext
    {
        public SCDataContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
            Database.SetInitializer<SCDataContext>(null);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;

            // Sets DateTimeKinds on DateTimes of Entities, so that Dates are automatically set to be UTC.
            // Currently only processes CleanEntityBase entities. All EntityBase entities remain unchanged.
            // http://stackoverflow.com/questions/4648540/entity-framework-datetime-and-utc
            //((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(u => u.RoleGroups)
                                                  .WithMany(r => r.Users)
                                                  .Map(m =>
                                                  {
                                                      m.MapLeftKey("UserId");
                                                      m.MapRightKey("RoleGroupId");
                                                      m.ToTable("UserGroups");
                                                  });

            modelBuilder.Entity<RoleGroup>().HasMany(u => u.Users)
                                                  .WithMany(r => r.RoleGroups)
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

            //modelBuilder.Entity<TeacherDivision>()
            //        .HasKey(c => new { c.DivisionId, c.TeacherId });

        }

        #region DECLARE TABLES        
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Timezone> Timezone { get; set; }

        public virtual DbSet<AppClaim> AppClaim { get; set; }
        public virtual DbSet<AppFunction> AppFunction { get; set; }

        public virtual DbSet<GroupClaim> GroupClaim { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<User> User { get; set; }
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
        //public virtual DbSet<CourseWeeklySchedule> CourseWeeklySchedule { get; set; }

        public virtual DbSet<MessagingDataMapping> MessagingDataMapping { get; set; }
        public virtual DbSet<MessagingMessage> MessagingMessage { get; set; }
        public virtual DbSet<MessagingTemplate> MessagingTemplate { get; set; }
        public virtual DbSet<MessagingTemplateContent> MessagingTemplateContent { get; set; }
        public virtual DbSet<MessagingType> MessagingType { get; set; }

        #endregion

        #region Extension
        public TEntity FindById<TEntity>(params object[] ids) where TEntity : class
        {
            return base.Set<TEntity>().Find(ids);
        }
        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class
        {
            var result = base.Set<TEntity>().Add(entity);

            var creationTrackingEntity = entity as IEntityTrackingCreation;
            if (creationTrackingEntity != null)
            {
                creationTrackingEntity.DateCreated = DateTime.UtcNow;
            }

            //((IObjectState)entity).State = ObjectState.Added;
            return result;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            base.Set<TEntity>().Attach(entity);

            var modifyTrackingEntity = entity as IEntityTrackingModified;
            if (modifyTrackingEntity != null)
            {
                modifyTrackingEntity.DateModified = DateTime.UtcNow;
            }

            //((IObjectState)entity).State = ObjectState.Modified;
        }

        public void Update<TEntity, TKey>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity :class, IEntity<TKey>
        {
            //base.Set<TEntity>().Attach(entity);
            //DbEntityEntry<TEntity> entry = base.Entry(entity);

            //foreach (var selector in properties)
            //{
            //    entry.Property(selector).IsModified = true;
            //}

            Dictionary<object, object> originalValues = new Dictionary<object, object>();
            TEntity entityToUpdate = base.Set<TEntity>().Find(entity.Id);

            foreach (var property in properties)
            {
                var val = base.Entry(entityToUpdate).Property(property).OriginalValue;
                originalValues.Add(property, val);
            }

            //base.Entry(entityToUpdate).State = EntityState.Detached;

            //base.Entry(entity).State = EntityState.Unchanged;
            foreach (var property in properties)
            {
                base.Entry(entity).Property(property).OriginalValue = originalValues[property];
                base.Entry(entity).Property(property).IsModified = true;
            }
        }

        public void Delete<TEntity>(params object[] ids) where TEntity : class
        {
            var entity = FindById<TEntity>(ids);
            //((IObjectState)entity).State = ObjectState.Deleted;
            Delete(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            base.Set<TEntity>().Attach(entity);
            base.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Get<TEntity>(string storedProcedureName, params object[] args) where TEntity : class
        {
            var query = Database.SqlQuery<TEntity>(storedProcedureName, args).ToList();
            foreach (var entity in query)
            {
                DateTimeKindAttribute.Apply(entity);
            }
            IQueryable<TEntity> result = query.AsQueryable();
            return result;
        }

        public int Execute(string sqlCommand)
        {
            return Database.ExecuteSqlCommand(sqlCommand);
        }

        public int Execute(string sqlCommand, params object[] args)
        {
            var result = Database.ExecuteSqlCommand(sqlCommand, args);
            return result;
        }

        public void BulkInsert<TEntity>(IList<TEntity> insertList, string tableName, IList<SqlBulkCopyColumnMapping> mapping, DataTable table) where TEntity : class
        {
            using (var connection = new SqlConnection(Database.Connection.ConnectionString))
            {
                connection.Open();

                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = tableName;

                    foreach (var columnMapping in mapping)
                    {
                        bulkCopy.ColumnMappings.Add(columnMapping);
                    }

                    bulkCopy.WriteToServer(table);
                }

                connection.Close();
            }
        }
        #endregion

        private bool _disposed;

        protected override void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
    }
}
