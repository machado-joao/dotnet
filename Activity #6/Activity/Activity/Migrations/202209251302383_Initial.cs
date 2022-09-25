namespace Activity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PROFILES",
                c => new
                    {
                        PROFILE_CODE = c.Int(nullable: false, identity: true),
                        PROFILE_DESCRIPTION = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.PROFILE_CODE);
            
            CreateTable(
                "dbo.USERS_PROFILES",
                c => new
                    {
                        UP_CODE = c.Int(nullable: false, identity: true),
                        USER_CODE = c.Int(nullable: false),
                        PROFILE_CODE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UP_CODE)
                .ForeignKey("dbo.PROFILES", t => t.PROFILE_CODE, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.USER_CODE, cascadeDelete: true)
                .Index(t => t.PROFILE_CODE)
                .Index(t => t.USER_CODE);
            
            CreateTable(
                "dbo.USERS",
                c => new
                    {
                        USER_CODE = c.Int(nullable: false, identity: true),
                        USER_NAME = c.String(nullable: false, maxLength: 200),
                        USER_EMAIL = c.String(nullable: false, maxLength: 200),
                        USER_PASSWORD = c.String(),
                    })
                .PrimaryKey(t => t.USER_CODE);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.USERS_PROFILES", "USER_CODE", "dbo.USERS");
            DropForeignKey("dbo.USERS_PROFILES", "PROFILE_CODE", "dbo.PROFILES");
            DropIndex("dbo.USERS_PROFILES", new[] { "USER_CODE" });
            DropIndex("dbo.USERS_PROFILES", new[] { "PROFILE_CODE" });
            DropTable("dbo.USERS");
            DropTable("dbo.USERS_PROFILES");
            DropTable("dbo.PROFILES");
        }
    }
}
