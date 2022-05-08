using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrasBio.Migrations
{
    public partial class BookingUserIdString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_UserId1",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_UserId1",
                table: "Booking");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30db2dc9-c713-4087-b334-e038de77a6fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d03ee015-d83d-47cc-85cf-5475657edc3a");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f196edd-258a-46a3-84c1-c823fa38ee40", "bba26112-4692-4ae2-8791-99afe275045d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2fb74c4-a7be-466c-be5e-7d035522314e", "77421e91-556f-4618-99c7-35fd44158607", "Visitor", "VISITOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                table: "Booking",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_UserId",
                table: "Booking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_UserId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_UserId",
                table: "Booking");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f196edd-258a-46a3-84c1-c823fa38ee40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2fb74c4-a7be-466c-be5e-7d035522314e");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Booking",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30db2dc9-c713-4087-b334-e038de77a6fb", "320d26e1-f4c6-4320-ba4d-1d205e393d2f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d03ee015-d83d-47cc-85cf-5475657edc3a", "6e7c882d-56e1-4f09-98ad-f9466d462580", "Visitor", "VISITOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId1",
                table: "Booking",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_UserId1",
                table: "Booking",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
