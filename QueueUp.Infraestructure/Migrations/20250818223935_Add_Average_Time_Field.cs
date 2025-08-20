using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueUp.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Average_Time_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageTime",
                table: "Establishments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageTime",
                table: "Establishments");
        }
    }
}
