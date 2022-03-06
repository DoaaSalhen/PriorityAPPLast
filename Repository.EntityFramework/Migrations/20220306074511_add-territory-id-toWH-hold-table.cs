using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class addterritoryidtoWHholdtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TerritoryId",
                table: "WarehouseOrderHold",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOrderHold_TerritoryId",
                table: "WarehouseOrderHold",
                column: "TerritoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOrderHold_Territories_TerritoryId",
                table: "WarehouseOrderHold",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOrderHold_Territories_TerritoryId",
                table: "WarehouseOrderHold");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOrderHold_TerritoryId",
                table: "WarehouseOrderHold");

            migrationBuilder.DropColumn(
                name: "TerritoryId",
                table: "WarehouseOrderHold");
        }
    }
}
