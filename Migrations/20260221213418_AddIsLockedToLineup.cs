using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFLFantasyChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AddIsLockedToLineup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Lineups");
        }
    }
}
