namespace SchedulerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        uId = c.Guid(nullable: false),
                        eventId = c.Int(nullable: false),
                        etDept = c.String(),
                        title = c.String(),
                        startt = c.DateTime(nullable: false),
                        endt = c.DateTime(nullable: false),
                        proCustomer = c.String(),
                        allDay = c.Boolean(nullable: false),
                        color = c.String(),
                        eDesc = c.String(),
                        trigPerson = c.String(),
                        PoNo = c.String(),
                        PoStatus = c.String(),
                        SONo = c.String(nullable: false),
                        SoStatus = c.String(),
                        JoNo = c.String(),
                        nowAtStatn = c.String(),
                        TotpartNo = c.Int(nullable: false),
                        comments = c.String(),
                        EventsStatus = c.String(),
                        backgColor = c.String(),
                        borderColor = c.String(),
                        textColor = c.String(),
                        dtCreated = c.DateTime(nullable: false),
                        SoNosEvent_id = c.Int(),
                    })
                .PrimaryKey(t => t.uId)
                .ForeignKey("dbo.EventsPartNos", t => t.SoNosEvent_id)
                .Index(t => t.SoNosEvent_id); 
            
            CreateTable(
                "dbo.EventsPartNos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        uUid = c.Guid(nullable: false),
                        SoNo = c.String(),
                        AddNewPartNo = c.Boolean(nullable: false),
                        MultiplePartNo = c.String(),
                        MultiplePartQtyNo = c.Int(nullable: false),
                        HCategory = c.String(),
                        HDia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HLength = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accessr = c.String(),
                        HLLength = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LeadProtect = c.String(),
                        Potting = c.String(),
                        Hdesc = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.FileUploadEvents",
                c => new
                    {
                        soCopyId = c.Int(nullable: false, identity: true),
                        soEventId = c.Guid(nullable: false),
                        soCopyType = c.String(),
                        soNoRef = c.String(),
                        soCopyData = c.Binary(),
                    })
                .PrimaryKey(t => t.soCopyId);
            
            CreateTable(
                "dbo.Users",
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
                        Accesslvl = c.String(),
                        Fname = c.String(),
                        Lname = c.String(),
                        LastLogin = c.DateTime(),
                        UserId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UsersInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserVerify = c.Int(nullable: false),
                        UserDept = c.String(),
                        UserFname = c.String(),
                        LastLogin = c.DateTime(nullable: false),
                        LastLogout = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex"); 
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Events", "SoNosEvent_id", "dbo.EventsPartNos");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Events", new[] { "SoNosEvent_id" });
            DropTable("dbo.Roles");
            DropTable("dbo.UsersInfo");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.FileUploadEvents");
            DropTable("dbo.EventsPartNos");
            DropTable("dbo.Events");
        }
    }
}
