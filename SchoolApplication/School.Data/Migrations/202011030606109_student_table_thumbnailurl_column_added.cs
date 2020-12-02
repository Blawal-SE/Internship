namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class student_table_thumbnailurl_column_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ThumbUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "ThumbUrl");
        }
    }
}
