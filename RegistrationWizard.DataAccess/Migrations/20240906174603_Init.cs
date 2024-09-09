using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistrationWizard.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdateDt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreateDt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdateDt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreateDt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdateDt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreateDt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1099), new TimeSpan(0, 0, 0, 0, 0)), "Country 1" },
                    { 2, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1108), new TimeSpan(0, 0, 0, 0, 0)), "Country 2" },
                    { 3, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1136), new TimeSpan(0, 0, 0, 0, 0)), "Country 3" },
                    { 4, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1141), new TimeSpan(0, 0, 0, 0, 0)), "Country 4" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "CreateDt", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1103), new TimeSpan(0, 0, 0, 0, 0)), "Province 2" },
                    { 2, 1, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1104), new TimeSpan(0, 0, 0, 0, 0)), "Province 3" },
                    { 3, 1, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1106), new TimeSpan(0, 0, 0, 0, 0)), "Province 4" },
                    { 4, 1, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1107), new TimeSpan(0, 0, 0, 0, 0)), "Province 5" },
                    { 5, 2, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1109), new TimeSpan(0, 0, 0, 0, 0)), "Province 6" },
                    { 6, 2, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1111), new TimeSpan(0, 0, 0, 0, 0)), "Province 7" },
                    { 7, 2, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1112), new TimeSpan(0, 0, 0, 0, 0)), "Province 8" },
                    { 8, 2, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1134), new TimeSpan(0, 0, 0, 0, 0)), "Province 9" },
                    { 9, 3, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1137), new TimeSpan(0, 0, 0, 0, 0)), "Province 10" },
                    { 10, 3, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1139), new TimeSpan(0, 0, 0, 0, 0)), "Province 11" },
                    { 11, 3, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1140), new TimeSpan(0, 0, 0, 0, 0)), "Province 12" },
                    { 12, 3, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1140), new TimeSpan(0, 0, 0, 0, 0)), "Province 13" },
                    { 13, 4, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1142), new TimeSpan(0, 0, 0, 0, 0)), "Province 14" },
                    { 14, 4, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1143), new TimeSpan(0, 0, 0, 0, 0)), "Province 15" },
                    { 15, 4, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1144), new TimeSpan(0, 0, 0, 0, 0)), "Province 16" },
                    { 16, 4, new DateTimeOffset(new DateTime(2024, 9, 6, 17, 46, 3, 552, DateTimeKind.Unspecified).AddTicks(1144), new TimeSpan(0, 0, 0, 0, 0)), "Province 17" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProvinceId",
                table: "Users",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
