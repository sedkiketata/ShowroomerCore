﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreMVC.Models;

namespace CoreMVC.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20170404225954_Removing-Purchase-Model")]
    partial class RemovingPurchaseModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:.uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("ProductVersion", "1.0.2");

            modelBuilder.Entity("CoreMVC.Models.Image", b =>
                {
                    b.Property<long>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("ProductId");

                    b.Property<string>("Url");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CoreMVC.Models.Interaction", b =>
                {
                    b.Property<long>("InteractionId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("UserId");

                    b.Property<long>("ProductId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("InteractionId", "UserId", "ProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Interaction");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Interaction");
                });

            modelBuilder.Entity("CoreMVC.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ProductId");

                    b.Property<long>("PurchaseId");

                    b.Property<int>("Quantity");

                    b.Property<long>("UserId");

                    b.HasKey("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CoreMVC.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<string>("Category");

                    b.Property<float>("Discount");

                    b.Property<string>("Name");

                    b.Property<float>("Price");

                    b.Property<int>("Quantity");

                    b.Property<float>("TVA");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CoreMVC.Models.Showroom", b =>
                {
                    b.Property<long>("ShowroomId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ProductId");

                    b.Property<long>("ShowroomerId");

                    b.HasKey("ShowroomId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShowroomerId");

                    b.ToTable("Showrooms");
                });

            modelBuilder.Entity("CoreMVC.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Street");

                    b.Property<string>("Username");

                    b.Property<int>("ZipCode");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("CoreMVC.Models.Voucher", b =>
                {
                    b.Property<long>("VoucherId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Reference");

                    b.Property<long>("UserId");

                    b.HasKey("VoucherId");

                    b.HasIndex("UserId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("CoreMVC.Models.Comment", b =>
                {
                    b.HasBaseType("CoreMVC.Models.Interaction");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Text");

                    b.ToTable("Comment");

                    b.HasDiscriminator().HasValue("Comment");
                });

            modelBuilder.Entity("CoreMVC.Models.Rate", b =>
                {
                    b.HasBaseType("CoreMVC.Models.Interaction");

                    b.Property<int>("Mark");

                    b.ToTable("Rate");

                    b.HasDiscriminator().HasValue("Rate");
                });

            modelBuilder.Entity("CoreMVC.Models.Buyer", b =>
                {
                    b.HasBaseType("CoreMVC.Models.User");

                    b.Property<string>("DeliveryAddress");

                    b.ToTable("Buyer");

                    b.HasDiscriminator().HasValue("Buyer");
                });

            modelBuilder.Entity("CoreMVC.Models.Showroomer", b =>
                {
                    b.HasBaseType("CoreMVC.Models.User");

                    b.Property<string>("Description");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.ToTable("Showroomer");

                    b.HasDiscriminator().HasValue("Showroomer");
                });

            modelBuilder.Entity("CoreMVC.Models.Image", b =>
                {
                    b.HasOne("CoreMVC.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreMVC.Models.Interaction", b =>
                {
                    b.HasOne("CoreMVC.Models.Product", "Product")
                        .WithMany("Interactions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreMVC.Models.User", "User")
                        .WithMany("Interactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreMVC.Models.Order", b =>
                {
                    b.HasOne("CoreMVC.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreMVC.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreMVC.Models.Showroom", b =>
                {
                    b.HasOne("CoreMVC.Models.Product", "Product")
                        .WithMany("Showrooms")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreMVC.Models.Showroomer", "Showroomer")
                        .WithMany("Showrooms")
                        .HasForeignKey("ShowroomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreMVC.Models.Voucher", b =>
                {
                    b.HasOne("CoreMVC.Models.User", "User")
                        .WithMany("Vouchers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
