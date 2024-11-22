using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDiscount",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "BasePrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "Products",
                newName: "Price");

            migrationBuilder.AddColumn<bool>(
                name: "HasDiscount",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
