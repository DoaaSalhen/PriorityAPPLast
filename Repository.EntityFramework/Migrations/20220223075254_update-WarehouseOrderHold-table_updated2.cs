using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class updateWarehouseOrderHoldtable_updated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseOrderHold",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId1 = table.Column<long>(type: "bigint", nullable: false),
                    HoldPriorityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HolduserId = table.Column<string>(type: "nvarchar(400)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOrderHold", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_WarehouseOrderHold_Holds_HoldPriorityDate_HolduserId",
                        columns: x => new { x.HoldPriorityDate, x.HolduserId },
                        principalTable: "Holds",
                        principalColumns: new[] { "PriorityDate", "userId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseOrderHold_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOrderHold_HoldPriorityDate_HolduserId",
                table: "WarehouseOrderHold",
                columns: new[] { "HoldPriorityDate", "HolduserId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOrderHold_OrderId",
                table: "WarehouseOrderHold",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseOrderHold");
        }
    }
}
