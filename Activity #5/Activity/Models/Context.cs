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
    }
}
