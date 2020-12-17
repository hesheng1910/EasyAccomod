using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class LikePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OfMod",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Notifications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UserLikePosts",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikePosts", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_UserLikePosts_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserLikePosts_PostId",
                table: "UserLikePosts",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikePosts");

            migrationBuilder.DropColumn(
                name: "OfMod",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "847c152c-f3f5-496d-87b7-b20df1cf2f20");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "3b45d0fe-45c9-4566-b09b-eaf699814e0e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "b75f5ac7-fa09-427e-9fe5-d397e30bf31d");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "ff3cb6e3-a685-41b8-9422-72200ed4d49d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b7ece5d3-d70f-4ab7-9ca0-05c99958214c", "AQAAAAEAACcQAAAAENJPQ64Ptj0B1VsS1Z2PhfdBqfoSVH6UOs52hBgT7nDK6VQUDNxPmHUDssOMdQ3xjA==" });
        }
    }
}
