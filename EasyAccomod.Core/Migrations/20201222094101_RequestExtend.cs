using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class RequestExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequsetExtendStatus",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "RequestExtends",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostOfExtend = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequsetExtendStatus = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestExtends", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "651095e9-2d45-4493-a07f-1e33ac88ca50");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "252db9c2-e9b6-49dc-a4eb-796abea964d5");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "cdae7706-c6d7-43bc-9747-aafb5b6499fc");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "53d83b43-af2f-44f5-82d8-4ed29276151d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b9bc2b7-d632-44cf-bca5-7c5fd2c8469d", "AQAAAAEAACcQAAAAEOTxQe1S4xaqSNzkbqSPYXUyhzA8dERen6rtUSb+ndMCkZlaxOA2qX8rqlDgvhdMCA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestExtends");

            migrationBuilder.AddColumn<short>(
                name: "RequsetExtendStatus",
                table: "Posts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "c680b4ee-4b42-4a4f-bb09-a30f3619b0d8");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "dd0f6791-f67c-4822-bd88-b967646175c0");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "e027c2f7-557e-47a2-9c11-46efcc99330c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "171f35c9-9b6e-460c-a8c0-214f45579a95");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1a9557e0-2ed9-46d3-893a-96e8fe935725", "AQAAAAEAACcQAAAAEBwlVF2LUTQg9YnTXNG/PEXeMidiy89ypNsBvyenRqYZp73so6h4LhIR215xQWlChg==" });
        }
    }
}
