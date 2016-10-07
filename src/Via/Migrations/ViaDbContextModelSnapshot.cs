using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Via.Data;

namespace Via.Migrations
{
    [DbContext(typeof(ViaDbContext))]
    partial class ViaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Via.Models.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BaptismChurch");

                    b.Property<DateTime?>("BaptismDate");

                    b.Property<string>("BaptismPastor");

                    b.Property<string>("BirthCity");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Dob");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsMember");

                    b.Property<string>("LastName");

                    b.Property<string>("Log");

                    b.Property<DateTime?>("MarriageDate");

                    b.Property<int?>("MarriedWithId");

                    b.Property<string>("Notes");

                    b.Property<int?>("ParentFatherId");

                    b.Property<int?>("ParentMotherId");

                    b.Property<string>("Phone");

                    b.Property<string>("Profession");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("Via.Models.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttendeeId");

                    b.Property<string>("Url");

                    b.HasKey("PictureId");

                    b.HasIndex("AttendeeId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Via.Models.Picture", b =>
                {
                    b.HasOne("Via.Models.Attendee", "Attendee")
                        .WithMany("Pictures")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
