using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddingStartEndLevelsAndAmountTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillingMethodUpchargeAmountType",
                table: "SkillingMethodUpcharge",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndLevel",
                table: "SkillingMethod",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartLevel",
                table: "SkillingMethod",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceUpchargeAmountType",
                table: "ServiceUpcharge",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillingMethodUpchargeAmountType",
                table: "SkillingMethodUpcharge");

            migrationBuilder.DropColumn(
                name: "EndLevel",
                table: "SkillingMethod");

            migrationBuilder.DropColumn(
                name: "StartLevel",
                table: "SkillingMethod");

            migrationBuilder.DropColumn(
                name: "ServiceUpchargeAmountType",
                table: "ServiceUpcharge");
        }
    }
}
