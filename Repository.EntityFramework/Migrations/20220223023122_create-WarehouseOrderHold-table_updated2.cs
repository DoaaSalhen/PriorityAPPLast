using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class createWarehouseOrderHoldtable_updated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorityDate",
                table: "WarehouseOrderHold");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "WarehouseOrderHold");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoldPriorityDate",
                table: "WarehouseOrderHold",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HoldPriorityDate",
                table: "WarehouseOrderHold",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "PriorityDate",
                table: "WarehouseOrderHold",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "WarehouseOrderHold",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
