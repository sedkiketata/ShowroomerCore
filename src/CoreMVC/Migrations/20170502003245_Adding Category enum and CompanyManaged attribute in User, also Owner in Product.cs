using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC.Migrations
{
    public partial class AddingCategoryenumandCompanyManagedattributeinUseralsoOwnerinProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyManaged",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyManaged",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Products");
        }
    }
}
