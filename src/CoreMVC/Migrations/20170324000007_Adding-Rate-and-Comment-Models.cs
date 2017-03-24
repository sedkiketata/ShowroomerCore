using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC.Migrations
{
    public partial class AddingRateandCommentModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Interaction",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Interaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Interaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Interaction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Interaction");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Interaction");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Interaction");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Interaction");
        }
    }
}
