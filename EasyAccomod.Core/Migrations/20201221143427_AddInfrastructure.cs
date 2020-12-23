using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAccomod.Core.Migrations
{
    public partial class AddInfrastructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressNearBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PublicTime",
                table: "Posts",
                newName: "ExpireTime");

            migrationBuilder.RenameColumn(
                name: "Infrastructure",
                table: "Posts",
                newName: "Description");

            migrationBuilder.AddColumn<long>(
                name: "AddressNearById",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InfrastructureId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDetele",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "PostStatus",
                table: "Posts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "RequsetExtendStatus",
                table: "Posts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WithOwner",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AddressNearBies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusStation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressNearBies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Infrastructures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirCond = table.Column<bool>(type: "bit", nullable: false),
                    Fridge = table.Column<bool>(type: "bit", nullable: false),
                    WaterHeater = table.Column<bool>(type: "bit", nullable: false),
                    ElecPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    Bath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kitchen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infrastructures", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressNearBies");

            migrationBuilder.DropTable(
                name: "Infrastructures");

            migrationBuilder.DropColumn(
                name: "AddressNearById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "InfrastructureId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsDetele",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostStatus",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RequsetExtendStatus",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "WithOwner",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ExpireTime",
                table: "Posts",
                newName: "PublicTime");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Posts",
                newName: "Infrastructure");

            migrationBuilder.AddColumn<string>(
                name: "AddressNearBy",
                table: "Posts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Posts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "98c01258-7dbe-4917-b273-21eb81094c2f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "8ecb794a-2c9d-41ca-ace3-8dee6161f2d4");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "7ee433ff-a654-4e15-86c6-e71e3a4e3ae0");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "55805ce0-fcc8-4297-9dce-e467aa3a73b3");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0670e408-beb5-4eda-a370-ebd81aad71d8", "AQAAAAEAACcQAAAAEJVrI2Ty1S1ZJS+BfH7lCMNxhZ/NZHeYOozP96IVrm4YYQhWpDbXWd9RCERkf6oikg==" });
        }
    }
}
