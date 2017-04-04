using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC.Migrations
{
    public partial class ChangeTheKeyofShowroomByAddingShowroomIdasKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Showrooms",
                table: "Showrooms");

            migrationBuilder.AddColumn<long>(
                name: "ShowroomId",
                table: "Showrooms",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showrooms",
                table: "Showrooms",
                columns: new[] { "ShowroomId", "ShowroomerId", "ProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Showrooms",
                table: "Showrooms");

            migrationBuilder.DropColumn(
                name: "ShowroomId",
                table: "Showrooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showrooms",
                table: "Showrooms",
                columns: new[] { "ShowroomerId", "ProductId" });
        }
    }
}
