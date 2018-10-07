namespace BloodGiverWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 15),
                        Email = c.String(),
                        Phone = c.String(),
                        Country = c.String(),
                        BloodGroup = c.String(),
                        ImagePath = c.String(),
                        Date = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BloodUsers");
        }
    }
}
