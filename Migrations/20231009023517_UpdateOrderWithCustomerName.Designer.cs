﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PHHWpos;

#nullable disable

namespace PHHWpos.Migrations
{
    [DbContext(typeof(PHHWposDbContext))]
    [Migration("20231009023517_UpdateOrderWithCustomerName")]
    partial class UpdateOrderWithCustomerName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ItemOrder", b =>
                {
                    b.Property<int>("ItemsId")
                        .HasColumnType("integer");

                    b.Property<int>("OrdersId")
                        .HasColumnType("integer");

                    b.HasKey("ItemsId", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("ItemOrder");
                });

            modelBuilder.Entity("PHHWpos.Models.Item", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pizza",
                            Price = 10
                        },
                        new
                        {
                            Id = 2,
                            Name = "Wings",
                            Price = 8
                        });
                });

            modelBuilder.Entity("PHHWpos.Models.Order", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .HasColumnType("text");

                    b.Property<long?>("CustomerPhone")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PaymentType")
                        .HasColumnType("text");

                    b.Property<bool?>("Status")
                        .HasColumnType("boolean");

                    b.Property<int?>("Tip")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerEmail = "johndoe@example.com",
                            CustomerName = "john doe",
                            CustomerPhone = 1234567890L,
                            DateClosed = new DateTime(2023, 10, 8, 21, 35, 17, 7, DateTimeKind.Local).AddTicks(784),
                            Name = "Order 1",
                            PaymentType = "Cash",
                            Status = true,
                            Tip = 2,
                            Type = "Phone",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerEmail = "janesmith@example.com",
                            CustomerName = "Jane Smith",
                            CustomerPhone = 9197025135L,
                            DateClosed = new DateTime(2023, 10, 8, 21, 35, 17, 7, DateTimeKind.Local).AddTicks(829),
                            Name = "Order 2",
                            PaymentType = "Credit Card",
                            Status = true,
                            Tip = 3,
                            Type = "In-Person",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PHHWpos.Models.Review", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ItemId = 1,
                            Rating = 5,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            ItemId = 2,
                            Rating = 4,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PHHWpos.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("UID")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mark",
                            UID = "b1s92873y7"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Alex",
                            UID = "n0c2389o4n"
                        });
                });

            modelBuilder.Entity("ItemOrder", b =>
                {
                    b.HasOne("PHHWpos.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PHHWpos.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PHHWpos.Models.Order", b =>
                {
                    b.HasOne("PHHWpos.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PHHWpos.Models.Review", b =>
                {
                    b.HasOne("PHHWpos.Models.Item", "Item")
                        .WithMany("Reviews")
                        .HasForeignKey("ItemId");

                    b.HasOne("PHHWpos.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PHHWpos.Models.Item", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PHHWpos.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
