using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NFLFantasyChallenge.Migrations.FantasyDbContextV2Migrations
{
    /// <inheritdoc />
    public partial class AddLineupStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Lineups");

            migrationBuilder.AddColumn<int>(
                name: "LineupStatusId",
                table: "Lineups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LineupStatuses",
                columns: table => new
                {
                    LineupStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineupStatuses", x => x.LineupStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_LineupStatusId",
                table: "Lineups",
                column: "LineupStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_LineupStatuses_LineupStatusId",
                table: "Lineups",
                column: "LineupStatusId",
                principalTable: "LineupStatuses",
                principalColumn: "LineupStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_LineupStatuses_LineupStatusId",
                table: "Lineups");

            migrationBuilder.DropTable(
                name: "LineupStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_LineupStatusId",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "LineupStatusId",
                table: "Lineups");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Lineups",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
