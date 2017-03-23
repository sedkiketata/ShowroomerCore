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
        public DbSet<Product> Products { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");

            // UserConfiguration
            builder.Entity<User>(user =>
                {
                    user.HasKey(id => id.UserId);
                }
            );

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
                }
            );

        }
       
    }
}
