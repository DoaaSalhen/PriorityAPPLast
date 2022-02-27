using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class addtypetoitemtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Items");
        }
    }
}
