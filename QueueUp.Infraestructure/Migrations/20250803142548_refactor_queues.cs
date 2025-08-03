using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueUp.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class refactor_queues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentRating_Establishments_EstablishmentId",
                table: "EstablishmentRating");

            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentRating_Users_UserId",
                table: "EstablishmentRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Queue_QueueId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueUser_Queue_QueueId",
                table: "QueueUser");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueUser_Users_UserId",
                table: "QueueUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueUser",
                table: "QueueUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Queue",
                table: "Queue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstablishmentRating",
                table: "EstablishmentRating");

            migrationBuilder.RenameTable(
                name: "QueueUser",
                newName: "QueueUsers");

            migrationBuilder.RenameTable(
                name: "Queue",
                newName: "Queues");

            migrationBuilder.RenameTable(
                name: "EstablishmentRating",
                newName: "EstablishmentRatings");

            migrationBuilder.RenameIndex(
                name: "IX_QueueUser_UserId",
                table: "QueueUsers",
                newName: "IX_QueueUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueUser_QueueId",
                table: "QueueUsers",
                newName: "IX_QueueUsers_QueueId");

            migrationBuilder.RenameIndex(
                name: "IX_EstablishmentRating_UserId",
                table: "EstablishmentRatings",
                newName: "IX_EstablishmentRatings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EstablishmentRating_EstablishmentId",
                table: "EstablishmentRatings",
                newName: "IX_EstablishmentRatings_EstablishmentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "QueueUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "QueueUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueUsers",
                table: "QueueUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Queues",
                table: "Queues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstablishmentRatings",
                table: "EstablishmentRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentRatings_Establishments_EstablishmentId",
                table: "EstablishmentRatings",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentRatings_Users_UserId",
                table: "EstablishmentRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Queues_QueueId",
                table: "Establishments",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueUsers_Queues_QueueId",
                table: "QueueUsers",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueUsers_Users_UserId",
                table: "QueueUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Queues_QueueId",
                table: "Users",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentRatings_Establishments_EstablishmentId",
                table: "EstablishmentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentRatings_Users_UserId",
                table: "EstablishmentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Queues_QueueId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueUsers_Queues_QueueId",
                table: "QueueUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueUsers_Users_UserId",
                table: "QueueUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Queues_QueueId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueUsers",
                table: "QueueUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Queues",
                table: "Queues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstablishmentRatings",
                table: "EstablishmentRatings");

            migrationBuilder.RenameTable(
                name: "QueueUsers",
                newName: "QueueUser");

            migrationBuilder.RenameTable(
                name: "Queues",
                newName: "Queue");

            migrationBuilder.RenameTable(
                name: "EstablishmentRatings",
                newName: "EstablishmentRating");

            migrationBuilder.RenameIndex(
                name: "IX_QueueUsers_UserId",
                table: "QueueUser",
                newName: "IX_QueueUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueUsers_QueueId",
                table: "QueueUser",
                newName: "IX_QueueUser_QueueId");

            migrationBuilder.RenameIndex(
                name: "IX_EstablishmentRatings_UserId",
                table: "EstablishmentRating",
                newName: "IX_EstablishmentRating_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EstablishmentRatings_EstablishmentId",
                table: "EstablishmentRating",
                newName: "IX_EstablishmentRating_EstablishmentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "QueueUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "QueueUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueUser",
                table: "QueueUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Queue",
                table: "Queue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstablishmentRating",
                table: "EstablishmentRating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentRating_Establishments_EstablishmentId",
                table: "EstablishmentRating",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentRating_Users_UserId",
                table: "EstablishmentRating",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Queue_QueueId",
                table: "Establishments",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueUser_Queue_QueueId",
                table: "QueueUser",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueUser_Users_UserId",
                table: "QueueUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id");
        }
    }
}
