using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreMVC.Models;

namespace CoreMVC.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20170403180807_Adding-First-Version-of-Purchase-Model-Without-Navigations-Properties")]
    partial class AddingFirstVersionofPurchaseModelWithoutNavigationsProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:.uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("ProductVersion", "1.0.2");

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

            modelBuilder.Entity("CoreMVC.Models.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePurchase");

                    b.Property<string>("Status");

                    b.Property<double>("Total");

                    b.HasKey("PurchaseId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("CoreMVC.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresse");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("User");
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
        }
    }
}
