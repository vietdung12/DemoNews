using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Data.Migrations
{
    public partial class fixImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdProduct",
                table: "Registers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "Images",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdProduct",
                table: "Registers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDefault",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));
        }
    }
}
