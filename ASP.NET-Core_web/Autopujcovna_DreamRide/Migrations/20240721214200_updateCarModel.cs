using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopujcovna_DreamRide.Migrations
{
    /// <inheritdoc />
    public partial class updateCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Requests",
                newName: "AdditionalInfo");

            migrationBuilder.AddColumn<string>(
                name: "TitleCarImage",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleCarImage",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "AdditionalInfo",
                table: "Requests",
                newName: "Note");
        }
    }
}
