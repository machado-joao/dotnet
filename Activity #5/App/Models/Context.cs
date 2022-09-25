using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Context : DbContext
    {
        public Context() : base(nameOrConnectionString: "ConnectionString") { }

        public DbSet<Book> Book { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<BookAuthor> BookAuthor { get; set; }

        public DbSet<Publisher> Publisher { get; set; }
    }
}
