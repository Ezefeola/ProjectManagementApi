using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapter.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ProjectUserRoleIndexed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserRole_Name",
                table: "ProjectUserRole",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectUserRole_Name",
                table: "ProjectUserRole");
        }
    }
}
