﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using retake_two.Models;

namespace retake_two.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20190827181321_firstMigration")]
    partial class firstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("retake_two.Models.Idea", b =>
                {
                    b.Property<int>("IdeaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idea_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("desciption");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("retake_two.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("like_id");

                    b.Property<int>("IdeaId")
                        .HasColumnName("idea_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("LikeId");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("retake_two.Models.UserReg", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnName("alias");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("pw");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("retake_two.Models.Idea", b =>
                {
                    b.HasOne("retake_two.Models.UserReg", "Creator")
                        .WithMany("Ideas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("retake_two.Models.Like", b =>
                {
                    b.HasOne("retake_two.Models.Idea", "Ideas")
                        .WithMany("Likes")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("retake_two.Models.UserReg", "Liker")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}