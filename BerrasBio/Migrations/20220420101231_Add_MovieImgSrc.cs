using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrasBio.Migrations
{
    public partial class Add_MovieImgSrc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_Source",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Source",
                table: "Movie");
        }
    }
}
