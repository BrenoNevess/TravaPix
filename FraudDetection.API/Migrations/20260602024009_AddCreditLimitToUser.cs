using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraudDetection.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditLimitToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Users",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Users");
        }
    }
}
