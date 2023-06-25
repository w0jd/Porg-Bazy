using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt.Migrations
{
    /// <inheritdoc />
    public partial class AddDaniaProdukty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Produktu",
                table: "Produkty",
                newName: "Id");

            migrationBuilder.AlterColumn<float>(
                name: "Węglowodany",
                table: "Produkty",
                type: "float",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "Tłuszcz",
                table: "Produkty",
                type: "float",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldMaxLength: 7,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "Kaloryczność",
                table: "Produkty",
                type: "float",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "Błonnik",
                table: "Produkty",
                type: "float",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldMaxLength: 7,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "Białko",
                table: "Produkty",
                type: "float",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldMaxLength: 6,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NazwaDania = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dania", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DaniaProdukty",
                columns: table => new
                {
                    IdProduktu = table.Column<int>(type: "int", nullable: false),
                    IdDania = table.Column<int>(type: "int", nullable: false),
                    Ilość = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dania");

            migrationBuilder.DropTable(
                name: "DaniaProdukty");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Produkty",
                newName: "ID_Produktu");

            migrationBuilder.AlterColumn<string>(
                name: "Węglowodany",
                table: "Produkty",
                type: "varchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldMaxLength: 11,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Tłuszcz",
                table: "Produkty",
                type: "varchar(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldMaxLength: 7,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Kaloryczność",
                table: "Produkty",
                type: "varchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldMaxLength: 12,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Błonnik",
                table: "Produkty",
                type: "varchar(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldMaxLength: 7,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Białko",
                table: "Produkty",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldMaxLength: 6,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
