namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    public partial class StudentTableuserasforeighnkeyadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "UserId");
            AddForeignKey("dbo.Students", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        public override void Down()
        {
            DropForeignKey("dbo.Students", "UserId", "dbo.Users");
            DropIndex("dbo.Students", new[] { "UserId" });
            DropColumn("dbo.Students", "UserId");
        }
    }
}
