using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mirosnicenco_Eugenia_Proiect.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "EnergyProvider",
                columns: table => new
                {
                    EnergyProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GreenEnergyShare = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyProvider", x => x.EnergyProviderId);
                });

            migrationBuilder.CreateTable(
                name: "Household",
                columns: table => new
                {
                    HouseholdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseholdSize = table.Column<int>(type: "int", nullable: false),
                    IncomeLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrbanRural = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Household", x => x.HouseholdId);
                    table.ForeignKey(
                        name: "FK_Household_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyUsage",
                columns: table => new
                {
                    EnergyUsageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseholdId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AvgMonthlyConsumption = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyUsage", x => x.EnergyUsageId);
                    table.ForeignKey(
                        name: "FK_EnergyUsage_Household_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Household",
                        principalColumn: "HouseholdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenewableSystem",
                columns: table => new
                {
                    RenewableSystemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseholdId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    SystemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacityKW = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstalationYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenewableSystem", x => x.RenewableSystemId);
                    table.ForeignKey(
                        name: "FK_RenewableSystem_EnergyProvider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "EnergyProvider",
                        principalColumn: "EnergyProviderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenewableSystem_Household_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Household",
                        principalColumn: "HouseholdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergyUsage_HouseholdId",
                table: "EnergyUsage",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Household_CountryId",
                table: "Household",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RenewableSystem_HouseholdId",
                table: "RenewableSystem",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_RenewableSystem_ProviderId",
                table: "RenewableSystem",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyUsage");

            migrationBuilder.DropTable(
                name: "RenewableSystem");

            migrationBuilder.DropTable(
                name: "EnergyProvider");

            migrationBuilder.DropTable(
                name: "Household");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
