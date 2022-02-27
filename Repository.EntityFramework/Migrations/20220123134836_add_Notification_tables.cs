using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class drop_Notification_tables : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersNotification");

            migrationBuilder.DropTable(
                name: "SubmitNotifications");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubmitNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSubmittedOrders = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false,defaultValue:false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    submitNotificationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Notification_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Notification_SubmitNotifications_submitNotificationId",
                        column: x => x.submitNotificationId,
                        principalTable: "SubmitNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Notification_OrderId",
                table: "OrderNotification",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Notification_submitNotificationId",
                table: "OrderNotification",
                column: "submitNotificationId");
        }
    }
}
