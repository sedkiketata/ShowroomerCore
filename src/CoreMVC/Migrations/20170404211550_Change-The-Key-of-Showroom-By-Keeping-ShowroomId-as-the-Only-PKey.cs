using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC.Migrations
{
    public partial class ChangeTheKeyofShowroomByKeepingShowroomIdastheOnlyPKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Showrooms",
              columns: table => new
              {
                  ShowroomId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                  ShowroomerId = table.Column<long>(nullable: false),
                  ProductId = table.Column<long>(nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Showrooms", x => x.ShowroomId);
                  table.ForeignKey(
                      name: "FK_Showrooms_Products_ProductId",
                      column: x => x.ProductId,
                      principalTable: "Products",
                      principalColumn: "ProductId",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_Showrooms_User_ShowroomerId",
                      column: x => x.ShowroomerId,
                      principalTable: "User",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.Cascade);
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Showrooms");
        }
    }
}
