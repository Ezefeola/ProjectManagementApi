using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapter.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ProjectUserRoleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "ProjectUser");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectUserRoleId",
                table: "ProjectUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProjectUserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_ProjectUserRoleId",
                table: "ProjectUser",
                column: "ProjectUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserRole_IsDeleted",
                table: "ProjectUserRole",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_ProjectUserRole_ProjectUserRoleId",
                table: "ProjectUser",
                column: "ProjectUserRoleId",
                principalTable: "ProjectUserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_ProjectUserRole_ProjectUserRoleId",
                table: "ProjectUser");

            migrationBuilder.DropTable(
                name: "ProjectUserRole");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUser_ProjectUserRoleId",
                table: "ProjectUser");

            migrationBuilder.DropColumn(
                name: "ProjectUserRoleId",
                table: "ProjectUser");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ProjectUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
