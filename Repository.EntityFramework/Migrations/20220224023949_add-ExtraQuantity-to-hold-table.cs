using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class addExtraQuantitytoholdtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ExtraQuantity",
                table: "Holds",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraQuantity",
                table: "Holds");
        }
    }
}
