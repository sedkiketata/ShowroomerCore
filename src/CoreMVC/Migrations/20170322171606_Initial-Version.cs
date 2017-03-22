using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC.Migrations
{
    public partial class InitialVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreatePostgresExtension("uuid-ossp");
            migrationBuilder.CreateTable(
                   name: "Products",
                   columns: table => new
                   {
                       ProductId = table.Column<Guid>(nullable: false)
                           .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                       Name = table.Column<string>(nullable: true),
                       Brand = table.Column<string>(nullable: true),
                       Category = table.Column<string>(nullable: true),
                       Discount = table.Column<float>(nullable: true),
                       Price = table.Column<float>(nullable: true),
                       Quantity = table.Column<int>(nullable: true),
                       TVA =table.Column<float>(nullable: true)
                   },
                   constraints: table =>
                   {
                       table.PrimaryKey("PK_Products", x => x.ProductId);
                   });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPostgresExtension("uuid-ossp");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
