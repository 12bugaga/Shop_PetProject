using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                schema: "Products",
                table: "product");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "Products",
                newName: "Products",
                newSchema: "product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "product",
                table: "Products",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "product",
                table: "Products");

            migrationBuilder.EnsureSchema(
                name: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "product",
                newName: "product",
                newSchema: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                schema: "Products",
                table: "product",
                column: "Id");
        }
    }
}
