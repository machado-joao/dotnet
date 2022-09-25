using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Context : DbContext
    {
        public Context() : base(nameOrConnectionString: "ConnectionString") { }

        public DbSet<User> User { get; set; }

        public DbSet<Profile> Profile { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            var user = mb.Entity<User>();
            user.ToTable("USERS");
            user.Property(x => x.Id).HasColumnName("USER_CODE");
            user.Property(x => x.Name).HasColumnName("USER_NAME");
            user.Property(x => x.Email).HasColumnName("USER_EMAIL");
            user.Property(x => x.Password).HasColumnName("USER_PASSWORD");

            var profile = mb.Entity<Profile>();
            profile.ToTable("PROFILES");
            profile.Property(x => x.Id).HasColumnName("PROFILE_CODE");
            profile.Property(x => x.Description).HasColumnName("PROFILE_DESCRIPTION");

            var userProfile = mb.Entity<UserProfile>();
            userProfile.ToTable("USERS_PROFILES");
            userProfile.Property(x => x.Id).HasColumnName("UP_CODE");
            userProfile.Property(x => x.UserId).HasColumnName("USER_CODE");
            userProfile.Property(x => x.ProfileId).HasColumnName("PROFILE_CODE");
        }
    }
}
