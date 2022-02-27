using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class addstatetowarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrdersNotification_Orders_OrderId",
            //    table: "OrdersNotification");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrdersNotification_SubmitNotifications_submitNotificationId",
            //    table: "OrdersNotification");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_OrdersNotification",
            //    table: "OrdersNotification");

            //migrationBuilder.RenameTable(
            //    name: "OrdersNotification",
            //    newName: "OrderNotification");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrdersNotification_submitNotificationId",
            //    table: "OrderNotification",
            //    newName: "IX_OrderNotification_submitNotificationId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrdersNotification_OrderId",
            //    table: "OrderNotification",
            //    newName: "IX_OrderNotification_OrderId");

            //migrationBuilder.AddColumn<bool>(
            //    name: "Seen",
            //    table: "SubmitNotifications",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_OrderNotification",
            //    table: "OrderNotification",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderNotification_Orders_OrderId",
            //    table: "OrderNotification",
            //    column: "OrderId",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderNotification_SubmitNotifications_submitNotificationId",
            //    table: "OrderNotification",
            //    column: "submitNotificationId",
            //    principalTable: "SubmitNotifications",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderNotification_Orders_OrderId",
            //    table: "OrderNotification");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderNotification_SubmitNotifications_submitNotificationId",
            //    table: "OrderNotification");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_OrderNotification",
            //    table: "OrderNotification");

            //migrationBuilder.DropColumn(
            //    name: "Seen",
            //    table: "SubmitNotifications");

            //migrationBuilder.RenameTable(
            //    name: "OrderNotification",
            //    newName: "OrdersNotification");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderNotification_submitNotificationId",
            //    table: "OrdersNotification",
            //    newName: "IX_OrdersNotification_submitNotificationId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderNotification_OrderId",
            //    table: "OrdersNotification",
            //    newName: "IX_OrdersNotification_OrderId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_OrdersNotification",
            //    table: "OrdersNotification",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrdersNotification_Orders_OrderId",
            //    table: "OrdersNotification",
            //    column: "OrderId",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrdersNotification_SubmitNotifications_submitNotificationId",
            //    table: "OrdersNotification",
            //    column: "submitNotificationId",
            //    principalTable: "SubmitNotifications",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
