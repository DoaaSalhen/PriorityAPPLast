using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class drop_order_Id_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
         name: "Orders");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
