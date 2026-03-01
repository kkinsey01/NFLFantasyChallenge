using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFLFantasyChallenge.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLineupSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_DefenseId",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_KickerId",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_QbId1",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_QbId2",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_RbId1",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_RbId2",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_RbId3",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_RbId4",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_RbId5",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_TeId1",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_TeId2",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_WrId1",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_WrId2",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_WrId3",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_WrId4",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_WrId5",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_DefenseId",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_KickerId",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_QbId1",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_QbId2",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_RbId1",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_RbId2",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_RbId3",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_RbId4",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_RbId5",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_TeId1",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_TeId2",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_WrId1",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_WrId2",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_WrId3",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_WrId4",
                table: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Lineups_WrId5",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "DefenseId",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "KickerId",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "QbId1",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "QbId2",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "RbId1",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "RbId2",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "RbId3",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "RbId4",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "RbId5",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "TeId1",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "TeId2",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "WrId1",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "WrId2",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "WrId3",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "WrId4",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "WrId5",
                table: "Lineups");

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    LineupSlotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    SlotIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.LineupSlotId);
                    table.ForeignKey(
                        name: "FK_Slots_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "LineupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slots_LineupId",
                table: "Slots",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_PlayerId",
                table: "Slots",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.AddColumn<int>(
                name: "DefenseId",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KickerId",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QbId1",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QbId2",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RbId1",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RbId2",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RbId3",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RbId4",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RbId5",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeId1",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeId2",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrId1",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrId2",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrId3",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrId4",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrId5",
                table: "Lineups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_DefenseId",
                table: "Lineups",
                column: "DefenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_KickerId",
                table: "Lineups",
                column: "KickerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_QbId1",
                table: "Lineups",
                column: "QbId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_QbId2",
                table: "Lineups",
                column: "QbId2");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_RbId1",
                table: "Lineups",
                column: "RbId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_RbId2",
                table: "Lineups",
                column: "RbId2");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_RbId3",
                table: "Lineups",
                column: "RbId3");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_RbId4",
                table: "Lineups",
                column: "RbId4");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_RbId5",
                table: "Lineups",
                column: "RbId5");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_TeId1",
                table: "Lineups",
                column: "TeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_TeId2",
                table: "Lineups",
                column: "TeId2");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_WrId1",
                table: "Lineups",
                column: "WrId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_WrId2",
                table: "Lineups",
                column: "WrId2");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_WrId3",
                table: "Lineups",
                column: "WrId3");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_WrId4",
                table: "Lineups",
                column: "WrId4");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_WrId5",
                table: "Lineups",
                column: "WrId5");

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_DefenseId",
                table: "Lineups",
                column: "DefenseId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_KickerId",
                table: "Lineups",
                column: "KickerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_QbId1",
                table: "Lineups",
                column: "QbId1",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_QbId2",
                table: "Lineups",
                column: "QbId2",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_RbId1",
                table: "Lineups",
                column: "RbId1",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_RbId2",
                table: "Lineups",
                column: "RbId2",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_RbId3",
                table: "Lineups",
                column: "RbId3",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_RbId4",
                table: "Lineups",
                column: "RbId4",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_RbId5",
                table: "Lineups",
                column: "RbId5",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_TeId1",
                table: "Lineups",
                column: "TeId1",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_TeId2",
                table: "Lineups",
                column: "TeId2",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_WrId1",
                table: "Lineups",
                column: "WrId1",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_WrId2",
                table: "Lineups",
                column: "WrId2",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_WrId3",
                table: "Lineups",
                column: "WrId3",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_WrId4",
                table: "Lineups",
                column: "WrId4",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_WrId5",
                table: "Lineups",
                column: "WrId5",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
