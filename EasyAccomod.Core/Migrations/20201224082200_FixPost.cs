using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class FixPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeRequest",
                table: "RequestExtends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ExpireTime",
                table: "Posts",
                newName: "PublicTime");

            migrationBuilder.AddColumn<int>(
                name: "RequestTime",
                table: "RequestExtends",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EffectiveTime",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DateViewPosts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateViewPosts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "4c7b7514-3ddf-46fc-b63a-88c925144c2f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "6e6ba9d2-2382-4f05-8ec4-cc0b492e616b");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "1ad5f16f-734d-43df-a7ea-f30bf8e78ed2");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "1d7f5203-1760-417c-b96a-9e8fb21a7b3d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "210e825d-537a-42e9-91fe-bb27fb05bd7a", "AQAAAAEAACcQAAAAEFTq4kXX8QHNR5wXTENBcnBsVXEz61JsFMsr1JolTXSmjEgBWzBJhUyUXOGDFCit2Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateViewPosts");

            migrationBuilder.DropColumn(
                name: "RequestTime",
                table: "RequestExtends");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EffectiveTime",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PublicTime",
                table: "Posts",
                newName: "ExpireTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeRequest",
                table: "RequestExtends",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Reports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "b722b79d-4414-47f8-918c-22c693242dfd");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "21b79323-e22a-4c9c-82c1-42a1cccf5cfc");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "9c17b235-01c9-43f5-a7e7-d545a1e03455");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "f6b1283f-2101-4656-a350-fd45f54ce7fd");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e41b6f72-1427-4e16-9f60-64b93a87e4c0", "AQAAAAEAACcQAAAAEMB5Fa1thzalBPUDq4k8zrWnmTT1DVfC/XYZikyWY2O8reSS5KTF67h9jhVWRjjnzQ==" });
        }
    }
}
