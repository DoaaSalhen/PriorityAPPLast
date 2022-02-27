using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class CreateWarehouseOrderHoldtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseOrderHold",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    PriorityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(400)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOrderHold", x => new { x.userId, x.PriorityDate, x.OrderId });
                    table.ForeignKey(
                        name: "FK_WarehouseOrderHold_Holds_PriorityDate_userId",
                        columns: x => new { x.PriorityDate, x.userId },
                        principalTable: "Holds",
                        principalColumns: new[] { "PriorityDate", "userId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseOrderHold_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOrderHold_OrderId",
                table: "WarehouseOrderHold",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOrderHold_PriorityDate_userId",
                table: "WarehouseOrderHold",
                columns: new[] { "PriorityDate", "userId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseOrderHold");
        }
    }
}
