﻿namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentTableemailcolumnaddedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Email");
        }
    }
}