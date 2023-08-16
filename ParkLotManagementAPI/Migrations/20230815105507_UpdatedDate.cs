using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkLotManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dailylogs_dailylogs_DailyLogsId",
                table: "dailylogs");

            migrationBuilder.DropIndex(
                name: "IX_dailylogs_DailyLogsId",
                table: "dailylogs");

            migrationBuilder.DropColumn(
                name: "DailyLogsId",
                table: "dailylogs");

            migrationBuilder.AddColumn<DateTime>(
                name: "checkIn",
                table: "dailylogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "checkOut",
                table: "dailylogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checkIn",
                table: "dailylogs");

            migrationBuilder.DropColumn(
                name: "checkOut",
                table: "dailylogs");

            migrationBuilder.AddColumn<int>(
                name: "DailyLogsId",
                table: "dailylogs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_dailylogs_DailyLogsId",
                table: "dailylogs",
                column: "DailyLogsId");

            migrationBuilder.AddForeignKey(
                name: "FK_dailylogs_dailylogs_DailyLogsId",
                table: "dailylogs",
                column: "DailyLogsId",
                principalTable: "dailylogs",
                principalColumn: "Id");
        }
    }
}
