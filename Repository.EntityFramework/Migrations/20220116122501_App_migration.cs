using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class App_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QutaCalc = table.Column<bool>(type: "bit", nullable: false),
                    HMP = table.Column<bool>(type: "bit", nullable: false),
                    OldItemNumber = table.Column<long>(type: "bigint", nullable: true),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubReions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainRegionId = table.Column<int>(type: "int", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubReions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubReions_MainRegions_MainRegionId",
                        column: x => x.MainRegionId,
                        principalTable: "MainRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SubregionId = table.Column<int>(type: "int", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_SubReions_SubregionId",
                        column: x => x.SubregionId,
                        principalTable: "SubReions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Territories_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    PriorityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    territoryId = table.Column<int>(type: "int", nullable: false),
                    QuotaQuantity = table.Column<int>(type: "int", nullable: false),
                    ReminingQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => new { x.PriorityDate, x.territoryId });
                    table.ForeignKey(
                        name: "FK_Holds_Territories_territoryId",
                        column: x => x.territoryId,
                        principalTable: "Territories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpdateQuotaHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OldQuantity = table.Column<int>(type: "int", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    territoryId = table.Column<int>(type: "int", nullable: true),
                    PriorityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateQuotaHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdateQuotaHistory_Territories_territoryId",
                        column: x => x.territoryId,
                        principalTable: "Territories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerritoryId = table.Column<int>(type: "int", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CutomerNameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    IsDelted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
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
                    PriorityId = table.Column<int>(type: "int", nullable: true),
                    PriorityQuantity = table.Column<int>(type: "int", nullable: true),
                    OrginalQuantity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Blocked = table.Column<bool>(type: "bit", nullable: true),
                    Dispatched = table.Column<bool>(type: "bit", nullable: true),
                    Submitted = table.Column<bool>(type: "bit", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ZoneId",
                table: "Customers",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_territoryId",
                table: "Holds",
                column: "territoryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_States_SubregionId",
                table: "States",
                column: "SubregionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubReions_MainRegionId",
                table: "SubReions",
                column: "MainRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_StateId",
                table: "Territories",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateQuotaHistory_territoryId",
                table: "UpdateQuotaHistory",
                column: "territoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_TerritoryId",
                table: "Zones",
                column: "TerritoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UpdateQuotaHistory");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Territories");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "SubReions");

            migrationBuilder.DropTable(
                name: "MainRegions");
        }
    }
}
