namespace App.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<App.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(App.Models.Context context)
        {
            context.Movie.AddOrUpdate(m => m.Title,
                new Models.Movie { Id = 1, Title = "Interestellar", Category = "Sci-Fi", Duration = 171, Synopsis = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival." },
                new Models.Movie { Id = 2, Title = "The Matrix", Category = "Sci-Fi", Duration = 136, Synopsis = "When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence." });
        }
    }
}
