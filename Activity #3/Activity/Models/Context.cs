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

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
