﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ServiceContext))]
    [Migration("20230912164217_Order")]
    partial class Order
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Categories", b =>
                {
                    b.Property<int>("IdCategories")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategories"));

                    b.Property<string>("CategoriesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCategories");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Entities.OrderItem", b =>
                {
                    b.Property<int>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrder"));

                    b.Property<bool>("Charged")
                        .HasColumnType("bit");

                    b.Property<bool>("Delivered")
                        .HasColumnType("bit");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOrder");

                    b.HasIndex("IdProduct");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Entities.ProductItem", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProduct"));

                    b.Property<int>("IdCategories")
                        .HasColumnType("int");

                    b.Property<string>("ProductImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productPrice")
                        .HasColumnType("int");

                    b.Property<int>("productStock")
                        .HasColumnType("int");

                    b.HasKey("IdProduct");

                    b.HasIndex("IdCategories");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Entities.RolItem", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("RolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRol");

                    b.ToTable("RollUser", (string)null);
                });

            modelBuilder.Entity("Entities.UserItem", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdRol");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Entities.OrderItem", b =>
                {
                    b.HasOne("Entities.ProductItem", null)
                        .WithMany()
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.UserItem", null)
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.ProductItem", b =>
                {
                    b.HasOne("Entities.Categories", null)
                        .WithMany()
                        .HasForeignKey("IdCategories")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.UserItem", b =>
                {
                    b.HasOne("Entities.RolItem", null)
                        .WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
