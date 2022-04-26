using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrasBio.Migrations
{
    public partial class awd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Bookable_Seats_SeatID_ShowID",
                table: "Bookable_Seats",
                columns: new[] { "SeatID", "ShowID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookable_Seats_SeatID_ShowID",
                table: "Bookable_Seats");
        }
    }
}
