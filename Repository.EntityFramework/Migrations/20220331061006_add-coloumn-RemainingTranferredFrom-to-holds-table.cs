﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.EntityFramework.Migrations
{
    public partial class addcoloumnRemainingTranferredFromtoholdstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RemainingTranferredFrom",
                table: "Holds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingTranferredFrom",
                table: "Holds");
        }
    }
}
