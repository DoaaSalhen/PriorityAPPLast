using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class adddeliverycoloumntoorders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "delivery",
                table: "Orders",
                type: "bit",
                nullable: true,
                defaultValue:true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "delivery",
                table: "Orders");
        }
    }
}
