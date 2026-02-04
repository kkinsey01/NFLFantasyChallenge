using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFLFantasyChallenge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Team = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    WildcardScore = table.Column<double>(type: "REAL", nullable: false),
                    DivisionalScore = table.Column<double>(type: "REAL", nullable: false),
                    ConferenceScore = table.Column<double>(type: "REAL", nullable: false),
                    SuperBowlScore = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<double>(type: "REAL", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    LineupId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QbId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    QbId2 = table.Column<int>(type: "INTEGER", nullable: false),
                    RbId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    RbId2 = table.Column<int>(type: "INTEGER", nullable: false),
                    RbId3 = table.Column<int>(type: "INTEGER", nullable: false),
                    RbId4 = table.Column<int>(type: "INTEGER", nullable: false),
                    RbId5 = table.Column<int>(type: "INTEGER", nullable: false),
                    WrId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    WrId2 = table.Column<int>(type: "INTEGER", nullable: false),
                    WrId3 = table.Column<int>(type: "INTEGER", nullable: false),
                    WrId4 = table.Column<int>(type: "INTEGER", nullable: false),
                    WrId5 = table.Column<int>(type: "INTEGER", nullable: false),
                    TeId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    TeId2 = table.Column<int>(type: "INTEGER", nullable: false),
                    KickerId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.LineupId);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_DefenseId",
                        column: x => x.DefenseId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_KickerId",
                        column: x => x.KickerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_QbId1",
                        column: x => x.QbId1,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_QbId2",
                        column: x => x.QbId2,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_RbId1",
                        column: x => x.RbId1,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_RbId2",
                        column: x => x.RbId2,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_RbId3",
                        column: x => x.RbId3,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_RbId4",
                        column: x => x.RbId4,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_RbId5",
                        column: x => x.RbId5,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_TeId1",
                        column: x => x.TeId1,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_TeId2",
                        column: x => x.TeId2,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_WrId1",
                        column: x => x.WrId1,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_WrId2",
                        column: x => x.WrId2,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_WrId3",
                        column: x => x.WrId3,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_WrId4",
                        column: x => x.WrId4,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_WrId5",
                        column: x => x.WrId5,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Lineups_UserId",
                table: "Lineups",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lineups");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
