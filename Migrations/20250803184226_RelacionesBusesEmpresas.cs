using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesBusesEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "company_id",
                table: "bus",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bus_company_id",
                table: "bus",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bus_companies_company_id",
                table: "bus",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bus_companies_company_id",
                table: "bus");

            migrationBuilder.DropIndex(
                name: "IX_bus_company_id",
                table: "bus");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "bus");
        }
    }
}
