using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mirosnicenco_Eugenia_Proiect.Migrations
{
    /// <inheritdoc />
    public partial class InitialPredictionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredictionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Household_Size = table.Column<int>(type: "int", nullable: false),
                    Income_Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urban_Rural = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adoption_Year = table.Column<int>(type: "int", nullable: false),
                    Monthly_Usage_kWh = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredictionHistory");
        }
    }
}
