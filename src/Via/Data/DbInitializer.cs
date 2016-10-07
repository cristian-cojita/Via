using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Via.Data;

namespace Via.Models
{
    public class DbInitializer
    {
        public static void Initialize(ViaDbContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Attendees.Any())
            {
                return;   // DB has been seeded
            }

            //var attendees = new Attendee[]
            //{
            //    new Attendee
            //    {
            //        Id =1,
            //        FirstName ="Cristian",
            //        LastName ="Cojita",
            //        Dob =DateTime.Parse("1979-06-23"),
            //        IsMember =true,
            //        Gender =Gender.M,
            //        CreatedAt=DateTime.Now,
            //        UpdatedAt=DateTime.Now
            //    },


            //    new Attendee
            //    {
            //        Id =2,
            //        FirstName ="Ramona",
            //        LastName ="Cojita",
            //        Dob =DateTime.Parse("1979-03-11"),
            //        IsMember =true,
            //        Gender =Gender.F,
            //        CreatedAt=DateTime.Now,
            //        UpdatedAt=DateTime.Now
            //    },

            //    new Attendee
            //    {
            //        Id =3,
            //        FirstName ="Doroteea",
            //        LastName ="Cojita",
            //        Dob =DateTime.Parse("20018-12-18"),
            //        ParentFatherId =1,
            //        ParentMotherId =2,
            //        Gender =Gender.F,
            //        CreatedAt=DateTime.Now,
            //        UpdatedAt=DateTime.Now
            //    },

            //    new Attendee
            //    {
            //        FirstName ="Rei",
            //        LastName ="Abrudan",
            //        Dob =DateTime.Parse("1969-06-23"),
            //        IsMember =true,
            //        Gender =Gender.M,
            //        CreatedAt=DateTime.Now,
            //        UpdatedAt=DateTime.Now
            //    },

            //    new Attendee
            //    {
            //        FirstName ="John",
            //        LastName ="John",
            //        Dob =DateTime.Parse("1979-01-01") ,
            //        Gender =Gender.M,
            //        CreatedAt=DateTime.Now,
            //        UpdatedAt=DateTime.Now
            //    }
            //};

            //foreach (Attendee s in attendees)
            //{
            //    context.Attendees.Add(s);
            //}
            //context.SaveChanges();
        }
    }
}
