using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSuccessApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Major = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Gpa = table.Column<double>(type: "REAL", precision: 3, scale: 2, nullable: false),
                    AttendancePercent = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedCredits = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCreditsRequired = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtracurricularPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    LastTermGpa = table.Column<double>(type: "REAL", precision: 3, scale: 2, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_Email",
                table: "StudentProfiles",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProfiles");
        }
    }
}
