using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopujcovna_DreamRide.Migrations
{
    /// <inheritdoc />
    public partial class updatecarattribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "EngineDisplacement",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.RenameColumn(
                name: "TypeOfCar",
                table: "Cars",
                newName: "Body");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "EngineDisplacement",
                table: "Cars",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Cars",
                newName: "TypeOfCar");
        }


    }
}
