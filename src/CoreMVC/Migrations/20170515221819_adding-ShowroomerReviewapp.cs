using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC.Migrations
{
    public partial class addingShowroomerReviewapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShowroomerReview",
                columns: table => new
                {
                    ShowroomerReviewId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Comment = table.Column<string>(nullable: true),
                    Mark = table.Column<int>(nullable: false),
                    ShowroomerId = table.Column<int>(nullable: false),
                    ShowroomerUserId = table.Column<long>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<long>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowroomerReview", x => x.ShowroomerReviewId);
                    table.ForeignKey(
                        name: "FK_ShowroomerReview_User_ShowroomerUserId",
                        column: x => x.ShowroomerUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShowroomerReview_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowroomerReview_ShowroomerUserId",
                table: "ShowroomerReview",
                column: "ShowroomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowroomerReview_UserId1",
                table: "ShowroomerReview",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowroomerReview");
        }
    }
}
