namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MOVIES",
                c => new
                    {
                        MOVIE_CODE = c.Int(nullable: false, identity: true),
                        MOVIE_TITLE = c.String(nullable: false, maxLength: 100),
                        MOVIE_CATEGORY = c.String(nullable: false, maxLength: 20),
                        MOVIE_DURATION = c.Int(nullable: false),
                        MOVIE_SYNOPSIS = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.MOVIE_CODE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MOVIES");
        }
    }
}
