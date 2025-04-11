﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobtrackerapi.Migrations
{
    /// <inheritdoc />
    public partial class ngotrackerdbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
