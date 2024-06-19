﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Crown.Infrastructure.Persistence.DbContexts;

#nullable disable

namespace Crown.Migrations.PostgreSQL.Migrations
{

    [DbContext(typeof(CrownDbContext))]
    partial class CrownDbContextModelSnapshot : ModelSnapshot
    {

        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.7.23375.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Crown.Domain.EventLogAggregate.EventLog", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("text");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserInfo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EventLog", (string)null);
                });

            modelBuilder.Entity("Crown.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BlockedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Crown.Domain.UserDataAggregate.UserData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FiredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("PreventUpdate")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("WorkingHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserData", (string)null);
                });

            modelBuilder.Entity("Crown.Domain.UserDataAggregate.UserData", b =>
                {
                    b.HasOne("Crown.Domain.UserAggregate.User", "User")
                        .WithMany("UserData")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Crown.Domain.UserDataAggregate.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserDataId")
                                .HasColumnType("uuid");

                            b1.Property<string>("HomeEmail")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("WorkEmail")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("UserDataId");

                            b1.ToTable("UserData");

                            b1.WithOwner()
                                .HasForeignKey("UserDataId");
                        });

                    b.OwnsOne("Crown.Domain.UserDataAggregate.ValueObjects.EmployeeNumber", "EmployeeNumber", b1 =>
                        {
                            b1.Property<Guid>("UserDataId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid");

                            b1.HasKey("UserDataId");

                            b1.ToTable("UserData");

                            b1.WithOwner()
                                .HasForeignKey("UserDataId");
                        });

                    b.OwnsOne("Crown.Domain.UserDataAggregate.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("UserDataId")
                                .HasColumnType("uuid");

                            b1.Property<string>("CityPhoneNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("InnerPhoneNumber")
                                .HasColumnType("text");

                            b1.Property<string>("MobilePhoneNumber")
                                .HasColumnType("text");

                            b1.HasKey("UserDataId");

                            b1.ToTable("UserData");

                            b1.WithOwner()
                                .HasForeignKey("UserDataId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("EmployeeNumber");

                    b.Navigation("Phone")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Crown.Domain.UserAggregate.User", b =>
                {
                    b.Navigation("UserData");
                });
#pragma warning restore 612, 618
        }
    }
}
