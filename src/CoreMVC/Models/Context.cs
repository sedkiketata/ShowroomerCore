/**
 * Copyright 2016 IBM Corp. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the “License”);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an “AS IS” BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Linq;

namespace CoreMVC.Models
{
    public class Context : DbContext
    {
        #region DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Showroomer> Showroomers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Showroom> Showrooms { get; set; }
        public DbSet<Image> Images { get; set; }
        #endregion

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");

            #region User Configuration

            // UserConfiguration
            builder.Entity<User>(user =>
                {
                    user.HasKey(id => id.UserId);
                    user.ToTable("User");
                }
            );
            #endregion

            #region Interaction Configuration

            // InteractionConfiguration
            builder.Entity<Interaction>(interaction =>
                    {
                        interaction.HasKey(i => new { i.InteractionId, i.UserId, i.ProductId });
                        interaction.HasOne<User>(i => i.User).WithMany(user => user.Interactions)
                                    .HasForeignKey(user => user.UserId)
                                    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                        interaction.HasOne<Product>(i => i.Product).WithMany(product => product.Interactions)
                                    .HasForeignKey(product => product.ProductId)
                                    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                        interaction.ToTable("Interaction");
                    }
                );
            #endregion

            #region Comment Configuration
            builder.Entity<Comment>()
                .ToTable("Comment");
            #endregion

            #region Rate Configuration
            builder.Entity<Rate>()
                .ToTable("Rate");
            #endregion

            #region Purchase Configuration
            builder.Entity<Purchase>( purchase =>
                    {
                        purchase.HasKey(p => p.PurchaseId);
                    }
                );
            #endregion
                
            #region Order Configuration
            builder.Entity<Order>(Order => 
                    {
                        Order.HasKey(o => o.OrderId);
                        Order.HasOne<Product>(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId)
                             .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                        Order.HasOne<Purchase>(o => o.Purchase).WithMany(p => p.Orders).HasForeignKey(o => o.PurchaseId)
                             .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                        Order.HasOne<User>(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId)
                             .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                    }
                );
            #endregion

            #region Buyer Configuration
            builder.Entity<Buyer>()
                .ToTable("Buyer");
            #endregion

            #region Showroomer Configuration
            builder.Entity<Showroomer>()
                .ToTable("Showroomer");
            #endregion

            #region Voucher Configuration
            builder.Entity<Voucher>(Voucher =>
                    {
                        Voucher.HasKey(v => v.VoucherId);
                        Voucher.HasOne(voucher => voucher.User).WithMany(sh => sh.Vouchers)
                                .HasForeignKey(v => v.UserId)
                                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                    }
                );
            #endregion

            #region Showroom Configuration
            builder.Entity<Showroom>(Showroom =>
                    {
                        Showroom.HasKey(showroom => showroom.ShowroomId);
                        Showroom.HasOne(showroom => showroom.Product).WithMany(p => p.Showrooms)
                                 .HasForeignKey(sh => sh.ProductId)
                                 .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                        Showroom.HasOne(showroom => showroom.Showroomer).WithMany(sh => sh.Showrooms)
                                 .HasForeignKey(sh => sh.ShowroomerId)
                                 .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
                    }
                );
            #endregion

            #region Image Configuration
            builder.Entity<Image>(Image =>
                {
                    Image.HasKey(image => image.ImageId);
                    Image.HasOne(image => image.Product).WithMany(p => p.Images)
                          .HasForeignKey(image => image.ProductId);
                }
            );
            #endregion

        }
       
    }
}
