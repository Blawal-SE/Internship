namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentTableIsCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FName = c.String(),
                        Phone = c.String(),
                        Dob = c.DateTime(nullable: false),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
