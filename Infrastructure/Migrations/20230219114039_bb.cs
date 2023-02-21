using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sarafi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Transactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ActivationDate", "DateOfBirth", "ExpirationDate", "Password", "Salt" },
                values: new object[] { new DateTime(2023, 2, 19, 16, 10, 38, 916, DateTimeKind.Local).AddTicks(6885), new DateTime(2023, 2, 19, 16, 10, 38, 916, DateTimeKind.Local).AddTicks(6872), new DateTime(2031, 2, 19, 16, 10, 38, 916, DateTimeKind.Local).AddTicks(6886), "0e6fbb0c5a793262f84b4a286aca82d6ebb8f4bb660e136920ea3d5ddee27dd732f74ffe199d2c55c283a0778143ef92d4250348301e03acd0b03a96f63214c2", "55db5b33d5c5fbbe79296f78f6e26948" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ActivationDate", "DateOfBirth", "ExpirationDate", "Password", "Salt" },
                values: new object[] { new DateTime(2023, 2, 19, 10, 33, 55, 974, DateTimeKind.Local).AddTicks(2872), new DateTime(2023, 2, 19, 10, 33, 55, 974, DateTimeKind.Local).AddTicks(2858), new DateTime(2031, 2, 19, 10, 33, 55, 974, DateTimeKind.Local).AddTicks(2873), "435da3d45fcff89a2b893329146f2af308159bddb675b17183286c010f047f029c14c0cc2858c5aaa96dab4f19554d8509225762a586230504d4377f17777a18", "3d0a9849200f780f0e682a4d3c4ab64f" });
        }
    }
}
