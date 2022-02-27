using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class adduserIdtohold2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holds_Territories_territoryId",
                table: "Holds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holds",
                table: "Holds");

            migrationBuilder.DropIndex(
                name: "IX_Holds_territoryId",
                table: "Holds");

            migrationBuilder.DropColumn(
                name: "territoryId",
                table: "Holds");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Holds",
                type: "nvarchar(400)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holds",
                table: "Holds",
                columns: new[] { "PriorityDate", "userId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Holds",
                table: "Holds");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Holds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "territoryId",
                table: "Holds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holds",
                table: "Holds",
                columns: new[] { "PriorityDate", "territoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Holds_territoryId",
                table: "Holds",
                column: "territoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_Territories_territoryId",
                table: "Holds",
                column: "territoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
