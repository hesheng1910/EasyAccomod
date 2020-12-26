using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class FixRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "4c44de9a-dfee-4136-8bd8-2870e49c73e7");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "f0ffd675-c01e-4e16-aa1c-c91942088adb");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "a4b45de7-fa44-4549-aa3d-9caf38cdf33c", "OWNER" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "d78e4d2b-7aa7-4f08-b775-c5c6a7376d94");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a7340c3-7b71-4ca9-a530-d97f76541aea", "AQAAAAEAACcQAAAAEPqLdKxpbXDN8OFBBhlPGjypO/QyNOQtrHMI5sXXtTxT9mSbhrKFFCOgNto6GuogsA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "1ad5f16f-734d-43df-a7ea-f30bf8e78ed2", "MODERATOR" });

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
    }
}
