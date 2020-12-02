using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
namespace School.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<School.Data.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(School.Data.SchoolContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(new List<Course> {
                new Course {Name="OOP"},
                new Course {Name="C#"},
                new Course {Name="Java"},
                new Course {Name="WEB"},
                new Course {Name="Asp.Net"},

                });
                context.SaveChanges();
            }
        }
    }
}