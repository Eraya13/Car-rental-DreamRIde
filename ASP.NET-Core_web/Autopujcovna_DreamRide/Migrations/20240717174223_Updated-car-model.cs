using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopujcovna_DreamRide.Migrations
{
    /// <inheritdoc />
    public partial class Updatedcarmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriveTrain",
                table: "Cars",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveTrain",
                table: "Cars");
        }
    }
}
