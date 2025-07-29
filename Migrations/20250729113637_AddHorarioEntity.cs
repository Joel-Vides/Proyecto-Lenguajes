using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Migrations
{
    /// <inheritdoc />
    public partial class AddHorarioEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "horarios",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    hora_salida = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    hora_llegada = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    ruta_id = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "horarios");
        }
    }
}
