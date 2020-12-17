using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class FixComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "78385630-6757-47ce-aaa9-ad23a8024c41");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "9033e74d-db58-4f1b-96db-2d7559ee6568");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "f8a05b7b-3624-4901-91a9-abee6d0a9410");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "64633d85-7d6c-439e-bf51-27129eaa471a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8f68cf5e-d10b-480b-8125-17a5f85d6d50", "AQAAAAEAACcQAAAAEPprsN9LkjIZedx9+Td0E3ZZqvMurRWuI0vivSpRFyCv0UuerN+sji2WYUnIHOBUTQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Notifications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "41d8adf1-09bd-4b89-a00e-5eb15da8e2f2");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "ec35c6a2-a4e2-416c-93ec-33c1f1754adf");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "6cf9d96e-dee4-4955-adf6-80c710cd5f1d");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "b5933d90-9f48-4d5c-9fc1-0401f4e3a3f0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7f8ea299-d3b0-471e-8c48-da4006acd062", "AQAAAAEAACcQAAAAEJcQrXQa7YNMt0Wn+h39+2oUU+GB1IUjM6KIANK9EbJ6RsKP/veQ6Cj7gB62UASSPg==" });
        }
    }
}
