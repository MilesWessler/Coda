namespace Coda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Tablatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        Content = c.String(),
                        Rating = c.Double(nullable: false),
                        TimeCreated = c.DateTime(nullable: false),
                        TotalRaters = c.Int(nullable: false),
                        AverageRating = c.Int(nullable: false),
                        PageViews = c.Int(nullable: false),
                        Message = c.String(),
                        MemberProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberProfiles", t => t.MemberProfile_Id)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.MemberProfile_Id);
            
            CreateTable(
                "dbo.MemberProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        UserName = c.String(),
                        AboutMe = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Rock = c.Boolean(nullable: false),
                        ClassicRock = c.Boolean(nullable: false),
                        PunkRock = c.Boolean(nullable: false),
                        Grunge = c.Boolean(nullable: false),
                        Metal = c.Boolean(nullable: false),
                        Blues = c.Boolean(nullable: false),
                        RAndB = c.Boolean(nullable: false),
                        Pop = c.Boolean(nullable: false),
                        Alternative = c.Boolean(nullable: false),
                        ConnectWithOtherMembers = c.Boolean(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubHeading = c.String(),
                        Author = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Body = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UploadedFile = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PricePerHour = c.Int(nullable: false),
                        Content = c.String(),
                        InstructorSince = c.DateTime(nullable: false),
                        Instrument = c.String(),
                        BusinessAddress = c.String(),
                        State = c.String(),
                        City = c.String(),
                        MemberProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MemberProfiles", t => t.MemberProfile_Id)
                .Index(t => t.MemberProfile_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SongScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TablatureRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TablatureID = c.Int(),
                        Rating = c.Double(nullable: false),
                        TotalRaters = c.Int(nullable: false),
                        AverageRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tablatures", t => t.TablatureID)
                .Index(t => t.TablatureID);
            
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TablatureRatings", "TablatureID", "dbo.Tablatures");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Instructors", "MemberProfile_Id", "dbo.MemberProfiles");
            DropForeignKey("dbo.Tablatures", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Tablatures", "MemberProfile_Id", "dbo.MemberProfiles");
            DropForeignKey("dbo.Songs", "ArtistId", "dbo.Artists");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TablatureRatings", new[] { "TablatureID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Instructors", new[] { "MemberProfile_Id" });
            DropIndex("dbo.Tablatures", new[] { "MemberProfile_Id" });
            DropIndex("dbo.Tablatures", new[] { "SongId" });
            DropIndex("dbo.Songs", new[] { "ArtistId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserRatings");
            DropTable("dbo.TablatureRatings");
            DropTable("dbo.SongScores");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Instructors");
            DropTable("dbo.Genres");
            DropTable("dbo.FileTables");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.MemberProfiles");
            DropTable("dbo.Tablatures");
            DropTable("dbo.Songs");
            DropTable("dbo.Artists");
        }
    }
}
