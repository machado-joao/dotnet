namespace Activity.Migrations
{
    using Activity.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Activity.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Activity.Models.Context context)
        {
            context.User.AddOrUpdate(p => p.Email,
                new Models.User { Id = 1, Name = "Allbert Almeida", Email = "allbert@fatec.com.br", Password = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==" },
                new Models.User { Id = 2, Name = "Bruno Silva", Email = "bruno@fatec.com.br", Password = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==" },
                new Models.User { Id = 3, Name = "Claudemir Santos", Email = "claudemir@fatec.com.br", Password = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==" },
                new Models.User { Id = 2, Name = "João Machado", Email = "joao@email.com", Password = "42db731b456f554fed3f9b9d48971e553d98afd8a1253228a6ec975fd6cc78ccc32ac05706469b1fd6c19c9aa15fbb391b47657d930d743fe871721b1a710b0e" });

            context.Profile.AddOrUpdate(p => p.Description,
                new Models.Profile { Id = 1, Description = "Admin" },
                new Models.Profile { Id = 2, Description = "Normal" },
                new Models.Profile { Id = 3, Description = "Manager" });

            context.UserProfile.AddOrUpdate(p => p.Id,
                new Models.UserProfile { Id = 1, UserId = 1, ProfileId = 1 },
                new Models.UserProfile { Id = 1, UserId = 1, ProfileId = 2 },
                new Models.UserProfile { Id = 1, UserId = 1, ProfileId = 3 },
                new Models.UserProfile { Id = 1, UserId = 2, ProfileId = 2 },
                new Models.UserProfile { Id = 1, UserId = 3, ProfileId = 3 });
        }
    }
}
