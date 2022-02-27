using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class removedeliverycoloumnfromordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_OrderCategories_OrderCategoryId",
            //    table: "Orders");

            //migrationBuilder.DropTable(
            //    name: "OrderCategories");

            //migrationBuilder.DropIndex(
            //    name: "IX_Orders_OrderCategoryId",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "OrderCategoryId",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "Truck",
            //    table: "Orders");

            migrationBuilder.DropColumn(
                name: "delivery",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "OrderCategoryId",
            //    table: "Orders",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Truck",
            //    table: "Orders",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "delivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.CreateTable(
            //    name: "OrderCategories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsDelted = table.Column<bool>(type: "bit", nullable: false),
            //        IsVisible = table.Column<bool>(type: "bit", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderCategories", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders_OrderCategoryId",
            //    table: "Orders",
            //    column: "OrderCategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_OrderCategories_OrderCategoryId",
            //    table: "Orders",
            //    column: "OrderCategoryId",
            //    principalTable: "OrderCategories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
