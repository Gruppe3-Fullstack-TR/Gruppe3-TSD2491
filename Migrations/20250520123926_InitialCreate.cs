using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gruppe3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Red = table.Column<float>(type: "REAL", nullable: false),
                    Green = table.Column<float>(type: "REAL", nullable: false),
                    Blue = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollenRegisterings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeOfPollen = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollenRegisterings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndexInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    IndexDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ColorInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ColorInfoId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndexInfos_ColorInfos_ColorInfoId",
                        column: x => x.ColorInfoId,
                        principalTable: "ColorInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndexInfos_ColorInfos_ColorInfoId1",
                        column: x => x.ColorInfoId1,
                        principalTable: "ColorInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlantInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    InSeason = table.Column<bool>(type: "INTEGER", nullable: false),
                    IndexInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantInfo_IndexInfos_IndexInfoId",
                        column: x => x.IndexInfoId,
                        principalTable: "IndexInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollenResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlantInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateInfoId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollenResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollenResponses_DateInfos_DateInfoId",
                        column: x => x.DateInfoId,
                        principalTable: "DateInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollenResponses_DateInfos_DateInfoId1",
                        column: x => x.DateInfoId1,
                        principalTable: "DateInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PollenResponses_PlantInfo_PlantInfoId",
                        column: x => x.PlantInfoId,
                        principalTable: "PlantInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndexInfos_ColorInfoId",
                table: "IndexInfos",
                column: "ColorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexInfos_ColorInfoId1",
                table: "IndexInfos",
                column: "ColorInfoId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlantInfo_IndexInfoId",
                table: "PlantInfo",
                column: "IndexInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PollenResponses_DateInfoId",
                table: "PollenResponses",
                column: "DateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PollenResponses_DateInfoId1",
                table: "PollenResponses",
                column: "DateInfoId1");

            migrationBuilder.CreateIndex(
                name: "IX_PollenResponses_PlantInfoId",
                table: "PollenResponses",
                column: "PlantInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollenRegisterings");

            migrationBuilder.DropTable(
                name: "PollenResponses");

            migrationBuilder.DropTable(
                name: "DateInfos");

            migrationBuilder.DropTable(
                name: "PlantInfo");

            migrationBuilder.DropTable(
                name: "IndexInfos");

            migrationBuilder.DropTable(
                name: "ColorInfos");
        }
    }
}
