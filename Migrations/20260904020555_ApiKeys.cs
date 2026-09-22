using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFLFantasyChallenge.Migrations.FantasyDbContextV2Migrations
{
    /// <inheritdoc />
    public partial class ApiKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RapidApiPlayerId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RapidApiTeamId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RapidApiPlayerId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RapidApiTeamId",
                table: "Players");
        }
    }
}
