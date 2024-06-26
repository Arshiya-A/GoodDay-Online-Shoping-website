﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MainProject.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeathTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Database.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("DelivaryPlace")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("OrderDate")
                        .HasMaxLength(300)
                        .HasColumnType("datetime2");

                    b.Property<int>("WareID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("WareID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Database.ShopingCart", b =>
                {
                    b.Property<int>("ShopingCartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopingCartID"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("WareID")
                        .HasColumnType("int");

                    b.HasKey("ShopingCartID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("OrderID");

                    b.ToTable("ShopingCarts");
                });

            modelBuilder.Entity("Database.Ware", b =>
                {
                    b.Property<int>("WareID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WareID"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfInsert")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(95)
                        .HasColumnType("nvarchar(95)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Visit")
                        .HasColumnType("int");

                    b.Property<string>("Walpapers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WareGroupID")
                        .HasColumnType("int");

                    b.Property<int>("WareSubGroupID")
                        .HasColumnType("int");

                    b.HasKey("WareID");

                    b.HasIndex("WareSubGroupID");

                    b.ToTable("Wares");
                });

            modelBuilder.Entity("Database.WareGroup", b =>
                {
                    b.Property<int>("WareGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WareGroupID"));

                    b.Property<string>("WareGroupName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("WareGroupID");

                    b.ToTable("WareGroups");
                });

            modelBuilder.Entity("Database.WareSubgroup", b =>
                {
                    b.Property<int>("WareSubGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WareSubGroupID"));

                    b.Property<int>("WareGroupID")
                        .HasColumnType("int");

                    b.Property<string>("WareSubGroupName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("WareSubGroupID");

                    b.HasIndex("WareGroupID");

                    b.ToTable("WareSubgroups");
                });

            modelBuilder.Entity("Database.Order", b =>
                {
                    b.HasOne("Database.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Ware", "Ware")
                        .WithMany("Orders")
                        .HasForeignKey("WareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("Database.ShopingCart", b =>
                {
                    b.HasOne("Database.Customer", "Customer")
                        .WithMany("ShopingCart")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Order", null)
                        .WithMany("ShopingCart")
                        .HasForeignKey("OrderID");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Database.Ware", b =>
                {
                    b.HasOne("Database.WareSubgroup", null)
                        .WithMany("Wares")
                        .HasForeignKey("WareSubGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.WareSubgroup", b =>
                {
                    b.HasOne("Database.WareGroup", "WareGroup")
                        .WithMany("WareSubgroups")
                        .HasForeignKey("WareGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WareGroup");
                });

            modelBuilder.Entity("Database.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShopingCart");
                });

            modelBuilder.Entity("Database.Order", b =>
                {
                    b.Navigation("ShopingCart");
                });

            modelBuilder.Entity("Database.Ware", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Database.WareGroup", b =>
                {
                    b.Navigation("WareSubgroups");
                });

            modelBuilder.Entity("Database.WareSubgroup", b =>
                {
                    b.Navigation("Wares");
                });
#pragma warning restore 612, 618
        }
    }
}
