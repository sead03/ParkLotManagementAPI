﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ParkLotManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dailylogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    subscriptionId = table.Column<int>(type: "integer", nullable: false),
                    checkIn = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    checkOut = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    DailyLogsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailylogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dailylogs_dailylogs_DailyLogsId",
                        column: x => x.DailyLogsId,
                        principalTable: "dailylogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "parkSpots",
                columns: table => new
                {
                    ParkSpotsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    reservedSpots = table.Column<int>(type: "integer", nullable: false),
                    freeSpots = table.Column<int>(type: "integer", nullable: false),
                    totalSpots = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parkSpots", x => x.ParkSpotsid);
                });

            migrationBuilder.CreateTable(
                name: "subscribers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    cardNumberId = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<int>(type: "integer", nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    plateNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscribers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<int>(type: "integer", nullable: false),
                    subscriberId = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Subscriptionsid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_subscriptions_subscriptions_Subscriptionsid",
                        column: x => x.Subscriptionsid,
                        principalTable: "subscriptions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "weekdaypriceplan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    hourlyPrice = table.Column<int>(type: "integer", nullable: false),
                    dailyPrice = table.Column<int>(type: "integer", nullable: false),
                    minimumHours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weekdaypriceplan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "weekendpriceplan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    hourlyPrice = table.Column<int>(type: "integer", nullable: false),
                    dailyPrice = table.Column<int>(type: "integer", nullable: false),
                    minimumHours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weekendpriceplan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dailylogs_DailyLogsId",
                table: "dailylogs",
                column: "DailyLogsId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_Subscriptionsid",
                table: "subscriptions",
                column: "Subscriptionsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailylogs");

            migrationBuilder.DropTable(
                name: "parkSpots");

            migrationBuilder.DropTable(
                name: "subscribers");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "weekdaypriceplan");

            migrationBuilder.DropTable(
                name: "weekendpriceplan");
        }
    }
}
