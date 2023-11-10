﻿// <auto-generated />
using System;
using Bank_Data_Web_Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bank_Data_Web_Service.Migrations
{
    [DbContext(typeof(DBManager))]
    partial class DBManagerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("Bank_Data_DLL.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountNo")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            AccountId = 1,
                            AccountNo = 6786887,
                            Balance = 99999.0,
                            Status = 0,
                            UserId = 2
                        },
                        new
                        {
                            AccountId = 2,
                            AccountNo = 2454567,
                            Balance = 4354.0,
                            Status = 0,
                            UserId = 2
                        },
                        new
                        {
                            AccountId = 3,
                            AccountNo = 567577,
                            Balance = 13214.0,
                            Status = 0,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Bank_Data_DLL.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Action")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogMessage")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.HasKey("LogId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Bank_Data_DLL.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Bank_Data_DLL.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Picture")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Address = "admin address",
                            Email = "admin@email.com",
                            Name = "admin",
                            Password = "adminpass",
                            Phone = 710000001,
                            Picture = "admin picture url",
                            Role = 1
                        },
                        new
                        {
                            UserId = 2,
                            Address = "user1 address",
                            Email = "user1@email.com",
                            Name = "user1",
                            Password = "user1pass",
                            Phone = 710000002,
                            Picture = "user1 picture url",
                            Role = 0
                        },
                        new
                        {
                            UserId = 3,
                            Address = "user2 address",
                            Email = "user2@email.com",
                            Name = "user2",
                            Password = "user2pass",
                            Phone = 710000002,
                            Picture = "user2 picture url",
                            Role = 0
                        });
                });

            modelBuilder.Entity("Bank_Data_DLL.Account", b =>
                {
                    b.HasOne("Bank_Data_DLL.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bank_Data_DLL.Transaction", b =>
                {
                    b.HasOne("Bank_Data_DLL.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Bank_Data_DLL.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Bank_Data_DLL.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
