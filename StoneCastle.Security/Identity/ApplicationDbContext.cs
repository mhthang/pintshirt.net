using Microsoft.AspNet.Identity.EntityFramework;
using StoneCastle.Domain.Authentication.Entities;
using StoneCastle.Security.Commons;
using System.Data.Entity;

namespace StoneCastle.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() 
        : base(Constants.ENTITY_FRAMEWORK_CONNECTION_STRING, throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<User>();

            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(x => x.PasswordHash).HasMaxLength(256);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.PhoneNumber).HasMaxLength(20);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.SecurityStamp).HasMaxLength(256);

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims").Property(x => x.ClaimType).HasMaxLength(500);
            modelBuilder.Entity<IdentityUserClaim>().Property(x => x.ClaimValue).HasMaxLength(500);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
