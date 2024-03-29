﻿// <auto-generated />
using System;
using GestionGastosBD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionGastosBD.Migrations
{
    [DbContext(typeof(GestionGastosBDContext))]
    partial class GestionGastosBDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionGastosBD.Models.Expenses", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<decimal>("cost")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("final_payment")
                        .HasColumnType("datetime2");

                    b.Property<string>("id_periodicity")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("next_payment")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("id_periodicity");

                    b.HasIndex("id_user");

                    b.ToTable("expenses", (string)null);
                });

            modelBuilder.Entity("GestionGastosBD.Models.Participants", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("net_monthly_salary")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("paymanets")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("id_user");

                    b.ToTable("participants", (string)null);
                });

            modelBuilder.Entity("GestionGastosBD.Models.PaymentMethods", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("periodicity")
                        .HasColumnType("int");

                    b.HasKey("name");

                    b.HasIndex("periodicity")
                        .IsUnique();

                    b.ToTable("payment_methods", (string)null);
                });

            modelBuilder.Entity("GestionGastosBD.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("GestionGastosBD.Models.Expenses", b =>
                {
                    b.HasOne("GestionGastosBD.Models.PaymentMethods", "PaymentMethods")
                        .WithMany()
                        .HasForeignKey("id_periodicity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionGastosBD.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethods");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("GestionGastosBD.Models.Participants", b =>
                {
                    b.HasOne("GestionGastosBD.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
