using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sarafi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bbf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ActivationDate", "DateOfBirth", "ExpirationDate", "Password", "Salt" },
                values: new object[] { new DateTime(2023, 2, 19, 16, 12, 29, 432, DateTimeKind.Local).AddTicks(6067), new DateTime(2023, 2, 19, 16, 12, 29, 432, DateTimeKind.Local).AddTicks(6054), new DateTime(2031, 2, 19, 16, 12, 29, 432, DateTimeKind.Local).AddTicks(6067), "87e9e48524598330707960f5efd7f80eb6b806624b12a1474dfbc36adb91dab3c062a04737cd904b6e654fd479bff1d75aee1062b033934b72bf0a54ed5fcb58", "cf9968256056e3130cb4b5ee7cf3f49a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ActivationDate", "DateOfBirth", "ExpirationDate", "Password", "Salt" },
                values: new object[] { new DateTime(2023, 2, 19, 16, 10, 38, 916, DateTimeKind.Local).AddTicks(6885), new DateTime(2023, 2, 19, 16, 10, 38, 916, DateTimeKind.Local).AddTicks(6872), new DateTime(2031, 2, 19, 16, 10, 38, 916, DateTimeKind.Local).AddTicks(6886), "0e6fbb0c5a793262f84b4a286aca82d6ebb8f4bb660e136920ea3d5ddee27dd732f74ffe199d2c55c283a0778143ef92d4250348301e03acd0b03a96f63214c2", "55db5b33d5c5fbbe79296f78f6e26948" });
        }
    }
}
