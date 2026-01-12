using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mirosnicenco_Eugenia_Proiect.Migrations
{
    /// <inheritdoc />
    public partial class ActualizareBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenewableSystem_EnergyProvider_ProviderId",
                table: "RenewableSystem");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Household");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "RenewableSystem",
                newName: "EnergyProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_RenewableSystem_ProviderId",
                table: "RenewableSystem",
                newName: "IX_RenewableSystem_EnergyProviderId");

            migrationBuilder.AlterColumn<double>(
                name: "CapacityKW",
                table: "RenewableSystem",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_RenewableSystem_EnergyProvider_EnergyProviderId",
                table: "RenewableSystem",
                column: "EnergyProviderId",
                principalTable: "EnergyProvider",
                principalColumn: "EnergyProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenewableSystem_EnergyProvider_EnergyProviderId",
                table: "RenewableSystem");

            migrationBuilder.RenameColumn(
                name: "EnergyProviderId",
                table: "RenewableSystem",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_RenewableSystem_EnergyProviderId",
                table: "RenewableSystem",
                newName: "IX_RenewableSystem_ProviderId");

            migrationBuilder.AlterColumn<decimal>(
                name: "CapacityKW",
                table: "RenewableSystem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Household",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RenewableSystem_EnergyProvider_ProviderId",
                table: "RenewableSystem",
                column: "ProviderId",
                principalTable: "EnergyProvider",
                principalColumn: "EnergyProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
