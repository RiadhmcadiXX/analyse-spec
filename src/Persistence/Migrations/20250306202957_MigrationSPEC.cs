using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationSPEC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "EufSuccess01",
                table: "Students",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EufSuccess02",
                table: "Students",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EufSuccess03",
                table: "Students",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EufSuccess04",
                table: "Students",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EufSuccess01",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EufSuccess02",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EufSuccess03",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EufSuccess04",
                table: "Students");
        }
    }
}
