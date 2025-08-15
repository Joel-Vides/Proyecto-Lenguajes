using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Migrations
{
    /// <inheritdoc />
    public partial class Relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bus_companies_company_id",
                table: "bus");

            migrationBuilder.AlterColumn<string>(
                name: "numero_ticket",
                table: "tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "HorarioId1",
                table: "tickets",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "horario_id",
                table: "tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "precio",
                table: "horarios",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "hora_salida",
                table: "horarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "hora_llegada",
                table: "horarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "bus_id",
                table: "horarios",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_id",
                table: "horarios",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ruta_id",
                table: "horarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ruta_id",
                table: "bus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_HorarioId1",
                table: "tickets",
                column: "HorarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_horarios_bus_id",
                table: "horarios",
                column: "bus_id");

            migrationBuilder.CreateIndex(
                name: "IX_horarios_company_id",
                table: "horarios",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_horarios_ruta_id",
                table: "horarios",
                column: "ruta_id");

            migrationBuilder.CreateIndex(
                name: "IX_bus_ruta_id",
                table: "bus",
                column: "ruta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bus_companies_company_id",
                table: "bus",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bus_ruta_ruta_id",
                table: "bus",
                column: "ruta_id",
                principalTable: "ruta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_horarios_bus_bus_id",
                table: "horarios",
                column: "bus_id",
                principalTable: "bus",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_horarios_companies_company_id",
                table: "horarios",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_horarios_ruta_ruta_id",
                table: "horarios",
                column: "ruta_id",
                principalTable: "ruta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_horarios_HorarioId1",
                table: "tickets",
                column: "HorarioId1",
                principalTable: "horarios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bus_companies_company_id",
                table: "bus");

            migrationBuilder.DropForeignKey(
                name: "FK_bus_ruta_ruta_id",
                table: "bus");

            migrationBuilder.DropForeignKey(
                name: "FK_horarios_bus_bus_id",
                table: "horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_horarios_companies_company_id",
                table: "horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_horarios_ruta_ruta_id",
                table: "horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_horarios_HorarioId1",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_HorarioId1",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_horarios_bus_id",
                table: "horarios");

            migrationBuilder.DropIndex(
                name: "IX_horarios_company_id",
                table: "horarios");

            migrationBuilder.DropIndex(
                name: "IX_horarios_ruta_id",
                table: "horarios");

            migrationBuilder.DropIndex(
                name: "IX_bus_ruta_id",
                table: "bus");

            migrationBuilder.DropColumn(
                name: "HorarioId1",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "horario_id",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "bus_id",
                table: "horarios");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "horarios");

            migrationBuilder.DropColumn(
                name: "ruta_id",
                table: "horarios");

            migrationBuilder.DropColumn(
                name: "ruta_id",
                table: "bus");

            migrationBuilder.AlterColumn<string>(
                name: "numero_ticket",
                table: "tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "precio",
                table: "horarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "hora_salida",
                table: "horarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "hora_llegada",
                table: "horarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_bus_companies_company_id",
                table: "bus",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id");
        }
    }
}
