using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class changeyeasterdayremainingtransfertoremainingtransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YesterdayRemainingTranferred",
                table: "Holds",
                newName: "RemainingTranferred");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingTranferred",
                table: "Holds",
                newName: "YesterdayRemainingTranferred");
        }
    }
}
