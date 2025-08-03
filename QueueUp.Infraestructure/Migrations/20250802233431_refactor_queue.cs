using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueUp.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class refactor_queue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Queue");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Queue");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Queue",
                newName: "Slots");

            migrationBuilder.CreateTable(
                name: "QueueUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueUser_Queue_QueueId",
                        column: x => x.QueueId,
                        principalTable: "Queue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueueUser_QueueId",
                table: "QueueUser",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueUser_UserId",
                table: "QueueUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "QueueUser");

            migrationBuilder.RenameColumn(
                name: "Slots",
                table: "Queue",
                newName: "Position");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Queue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Queue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
