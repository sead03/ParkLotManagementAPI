using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkLotManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedToSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_subscriptions_Subscriptionsid",
                table: "subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_Subscriptionsid",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "Subscriptionsid",
                table: "subscriptions");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "subscriptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "subscribers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "subscribers");

            migrationBuilder.AddColumn<int>(
                name: "Subscriptionsid",
                table: "subscriptions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_Subscriptionsid",
                table: "subscriptions",
                column: "Subscriptionsid");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_subscriptions_Subscriptionsid",
                table: "subscriptions",
                column: "Subscriptionsid",
                principalTable: "subscriptions",
                principalColumn: "id");
        }
    }
}
