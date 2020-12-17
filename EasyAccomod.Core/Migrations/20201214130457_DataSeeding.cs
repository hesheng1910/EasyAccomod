using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AppUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Images",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, "187e9a1d-39d0-4d74-805a-00446278174f", "Adminstrator Role", "ADMIN", null },
                    { 2L, "d6687f42-5d44-49e7-9225-81ff8e83447e", "Employee Role", "MODERATOR", null },
                    { 3L, "dd835425-073e-417b-83f7-de14fdc55036", "Owner Role", "OWNER", null },
                    { 4L, "98e7926c-cb1d-459b-b1e9-7a2e5a961d87", "Renter Role", "RENTER", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IdentityNumber", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "Tran Thai Tong", "6d810d34-df30-4b18-86be-dd776a84a5f8", null, false, "Hoa", null, "Nguyen", false, null, null, null, "AQAAAAEAACcQAAAAEHAqFlEmSTFY9MW+lVd+UHidmX9gZvpGdDUeANm2CZ7gozOyujEUowzBhiAP6QqRXQ==", null, false, null, false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Images",
                table: "Posts",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
