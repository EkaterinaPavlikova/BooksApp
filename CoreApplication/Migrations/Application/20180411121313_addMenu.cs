using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreApplication.Migrations.Application
{
    public partial class addMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessField",
                table: "RoleIdentityAccesses");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationMenuId",
                table: "RoleIdentityAccesses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationMenuTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMenus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleIdentityAccesses_ApplicationMenuId",
                table: "RoleIdentityAccesses",
                column: "ApplicationMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleIdentityAccesses_ApplicationMenus_ApplicationMenuId",
                table: "RoleIdentityAccesses",
                column: "ApplicationMenuId",
                principalTable: "ApplicationMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleIdentityAccesses_ApplicationMenus_ApplicationMenuId",
                table: "RoleIdentityAccesses");

            migrationBuilder.DropTable(
                name: "ApplicationMenus");

            migrationBuilder.DropIndex(
                name: "IX_RoleIdentityAccesses_ApplicationMenuId",
                table: "RoleIdentityAccesses");

            migrationBuilder.DropColumn(
                name: "ApplicationMenuId",
                table: "RoleIdentityAccesses");

            migrationBuilder.AddColumn<string>(
                name: "AccessField",
                table: "RoleIdentityAccesses",
                nullable: true);
        }
    }
}
