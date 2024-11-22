using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "Discounts",
                newName: "DiscountPercentage");

            migrationBuilder.AddColumn<DateTime>(
                name: "DiscountExpireDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountExpireDate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "Discounts",
                newName: "DiscountValue");
        }
    }
}
