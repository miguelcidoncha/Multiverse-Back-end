using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProductItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productPrice",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "productStock",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Products",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "ProductImageURL",
                table: "Products",
                newName: "name");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Products",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "productStock");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "ProductImageURL");

            migrationBuilder.AddColumn<int>(
                name: "productPrice",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
