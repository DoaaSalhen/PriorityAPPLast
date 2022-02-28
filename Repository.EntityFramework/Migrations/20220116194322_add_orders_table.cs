using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Repository.EntityFramework.Migrations
{
    public partial class add_orders_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Orders",
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
                   OrderNumber = table.Column<long>(type: "bigint", nullable: true),
                   PriorityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                   CustomerId = table.Column<long>(type: "bigint", nullable: true),
                   OrderDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   LineID = table.Column<int>(type: "int", nullable: true),
                   OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                   ItemId = table.Column<long>(type: "bigint", nullable: true),
                   PODNumber = table.Column<long>(type: "bigint", nullable: false),
                   PODName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   PODZoneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   PODZoneState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   PODZoneAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   OrderQuantity = table.Column<int>(type: "int", nullable: true),
                   PriorityId = table.Column<int>(type: "int", nullable: true, defaultValue: 2),
                   PriorityQuantity = table.Column<int>(type: "int", nullable: true),
                   OrginalQuantity = table.Column<int>(type: "int", nullable: true),
                   Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   Dispatched = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                   Submitted = table.Column<bool>(type: "bit", nullable: true, defaultValue:false),
                   SubmitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                   SDMCU = table.Column<int>(type: "int", nullable: true),
                   WHSubmittedID = table.Column<int>(type: "int", nullable: true),
                   Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Orders", x => x.Id);
                   table.ForeignKey(
                       name: "FK_Orders_Customers_CustomerId",
                       column: x => x.CustomerId,
                       principalTable: "Customers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_Orders_Items_ItemId",
                       column: x => x.ItemId,
                       principalTable: "Items",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_Orders_Priorities_PriorityId",
                       column: x => x.PriorityId,
                       principalTable: "Priorities",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "Order_CustomerId_idx",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "Order_item_idx",
                table: "Orders",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "Order_OrderDate_idx",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "Order_PriorityId_idx",
                table: "Orders",
                column: "PriorityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
