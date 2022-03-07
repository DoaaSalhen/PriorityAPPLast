using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class addterritoryidtoWHholdtable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WarehouseOrderHold_Territories_TerritoryId",
            //    table: "WarehouseOrderHold");

            //migrationBuilder.DropIndex(
            //    name: "IX_WarehouseOrderHold_TerritoryId",
            //    table: "WarehouseOrderHold");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateIndex(
            //    name: "IX_WarehouseOrderHold_TerritoryId",
            //    table: "WarehouseOrderHold",
            //    column: "TerritoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WarehouseOrderHold_Territories_TerritoryId",
            //    table: "WarehouseOrderHold",
            //    column: "TerritoryId",
            //    principalTable: "Territories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
