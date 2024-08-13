using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCrudApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomer3Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CUstomer_Id",
                table: "Customers",
                newName: "Customer_Id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "Customer_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_Id",
                table: "Customers",
                newName: "CUstomer_Id");

            migrationBuilder.RenameColumn(
                name: "Customer_Name",
                table: "Customers",
                newName: "Name");
        }
    }
}
