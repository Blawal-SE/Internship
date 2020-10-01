namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dobChangedToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Dob", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Dob", c => c.DateTime(nullable: false));
        }
    }
}
