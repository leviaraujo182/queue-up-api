using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueUp.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class refactor_queueid_establishment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentRating_Establishments_EstablishmentId",
                table: "EstablishmentRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Queue_QueueId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Users_UserId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Establishments_QueueId",
                table: "Establishments");

            migrationBuilder.AlterColumn<Guid>(
                name: "QueueId",
                table: "Establishments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Establishments_QueueId",
                table: "Establishments",
                column: "QueueId",
                unique: true,
                filter: "[QueueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentRating_Establishments_EstablishmentId",
                table: "EstablishmentRating",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Queue_QueueId",
                table: "Establishments",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Users_UserId",
                table: "Establishments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentRating_Establishments_EstablishmentId",
                table: "EstablishmentRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Queue_QueueId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Users_UserId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Establishments_QueueId",
                table: "Establishments");

            migrationBuilder.AlterColumn<Guid>(
                name: "QueueId",
                table: "Establishments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Establishments_QueueId",
                table: "Establishments",
                column: "QueueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentRating_Establishments_EstablishmentId",
                table: "EstablishmentRating",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Queue_QueueId",
                table: "Establishments",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Users_UserId",
                table: "Establishments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Queue_QueueId",
                table: "Users",
                column: "QueueId",
                principalTable: "Queue",
                principalColumn: "Id");
        }
    }
}
