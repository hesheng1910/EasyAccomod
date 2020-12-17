using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class RoomCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AppUsers_UserId1",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId1",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "RoomCategory",
                table: "Posts",
                newName: "RoomCategoryId");

            migrationBuilder.RenameColumn(
                name: "Property",
                table: "Posts",
                newName: "Infrastructure");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Reports",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<long>(
                name: "AppUserId",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RoomCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCategorys", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "847c152c-f3f5-496d-87b7-b20df1cf2f20", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "3b45d0fe-45c9-4566-b09b-eaf699814e0e", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b75f5ac7-fa09-427e-9fe5-d397e30bf31d", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "ff3cb6e3-a685-41b8-9422-72200ed4d49d", "RENTER" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "IsConfirm", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7ece5d3-d70f-4ab7-9ca0-05c99958214c", true, "admin", "AQAAAAEAACcQAAAAENJPQ64Ptj0B1VsS1Z2PhfdBqfoSVH6UOs52hBgT7nDK6VQUDNxPmHUDssOMdQ3xjA==", "" });

            migrationBuilder.InsertData(
                table: "RoomCategorys",
                columns: new[] { "Id", "RoomCategoryName" },
                values: new object[,]
                {
                    { 1, "Nhà trọ" },
                    { 2, "Chung Cư Mini" },
                    { 3, "Nhà Nguyên Căn" },
                    { 4, "Chung cư" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AppUsers_AppUserId",
                table: "Posts",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AppUsers_AppUserId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "RoomCategorys");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "RoomCategoryId",
                table: "Posts",
                newName: "RoomCategory");

            migrationBuilder.RenameColumn(
                name: "Infrastructure",
                table: "Posts",
                newName: "Property");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Reports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                table: "Comments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "187e9a1d-39d0-4d74-805a-00446278174f", null });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "d6687f42-5d44-49e7-9225-81ff8e83447e", null });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "dd835425-073e-417b-83f7-de14fdc55036", null });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "98e7926c-cb1d-459b-b1e9-7a2e5a961d87", null });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "IsConfirm", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d810d34-df30-4b18-86be-dd776a84a5f8", false, null, "AQAAAAEAACcQAAAAEHAqFlEmSTFY9MW+lVd+UHidmX9gZvpGdDUeANm2CZ7gozOyujEUowzBhiAP6QqRXQ==", null });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId1",
                table: "Reports",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AppUsers_UserId1",
                table: "Reports",
                column: "UserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
