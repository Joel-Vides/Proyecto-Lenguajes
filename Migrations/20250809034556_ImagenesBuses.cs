using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Migrations
{
    /// <inheritdoc />
    public partial class ImagenesBuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "bus",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "bus");
        }
    }
}
