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

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            var movie = mb.Entity<Movie>();
            movie.ToTable("MOVIES");
            movie.Property(x => x.Id).HasColumnName("MOVIE_CODE");
            movie.Property(x => x.Title).HasColumnName("MOVIE_TITLE");
            movie.Property(x => x.Category).HasColumnName("MOVIE_CATEGORY");
            movie.Property(x => x.Duration).HasColumnName("MOVIE_DURATION");
            movie.Property(x => x.Synopsis).HasColumnName("MOVIE_SYNOPSIS");
        }
    }
}
