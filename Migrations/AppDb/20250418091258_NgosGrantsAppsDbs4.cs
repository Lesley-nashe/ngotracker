using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngotracker.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class NgosGrantsAppsDbs4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NgoId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SubmissiDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationModels_GrantModels_GrantId",
                        column: x => x.GrantId,
                        principalTable: "GrantModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationModels_NgoModels_NgoId",
                        column: x => x.NgoId,
                        principalTable: "NgoModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationModels_GrantId",
                table: "ApplicationModels",
                column: "GrantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationModels_NgoId",
                table: "ApplicationModels",
                column: "NgoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationModels");
        }
    }
}
