using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Questions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cegep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstLogOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmitedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AdmEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OthStuQc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MGS = table.Column<byte>(type: "tinyint", nullable: false),
                    NumEstabSec = table.Column<int>(type: "int", nullable: false),
                    AdmTurn = table.Column<byte>(type: "tinyint", nullable: false),
                    Choice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cohort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Population = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    CollSess01 = table.Column<int>(type: "int", nullable: false),
                    CollSess02 = table.Column<int>(type: "int", nullable: false),
                    CollSess03 = table.Column<int>(type: "int", nullable: false),
                    CollSess04 = table.Column<int>(type: "int", nullable: false),
                    ProgSess01 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgSess02 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgSess03 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgSess04 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbFollow01 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbFollow02 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbFollow03 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbFollow04 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbSuccess01 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbSuccess02 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbSuccess03 = table.Column<byte>(type: "tinyint", nullable: false),
                    NbSuccess04 = table.Column<byte>(type: "tinyint", nullable: false),
                    ProgDec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearDec = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstAndLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<int>(type: "int", nullable: false),
                    Genmels = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sec5Fra = table.Column<byte>(type: "tinyint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetYear = table.Column<int>(type: "int", nullable: false),
                    TargetSemester = table.Column<int>(type: "int", nullable: false),
                    FreqType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    PresType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    CollSemTarget = table.Column<int>(type: "int", nullable: false),
                    ProgSemTarget = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reinscription = table.Column<bool>(type: "bit", nullable: false),
                    SuccessContract = table.Column<bool>(type: "bit", nullable: false),
                    TeachingServices = table.Column<byte>(type: "tinyint", nullable: false),
                    Sec5Eng = table.Column<byte>(type: "tinyint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questions_IdQuestionId",
                        column: x => x.IdQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Options_IdOptionId",
                        column: x => x.IdOptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Students_IdStudentId",
                        column: x => x.IdStudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_IdOptionId",
                table: "Answers",
                column: "IdOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_IdStudentId",
                table: "Answers",
                column: "IdStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_IdQuestionId",
                table: "Options",
                column: "IdQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ParentId",
                table: "Questions",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
