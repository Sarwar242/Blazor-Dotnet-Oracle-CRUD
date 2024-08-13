using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCrudApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Auth2ndAt",
                table: "Customers",
                newName: "MakeDt");

            migrationBuilder.RenameColumn(
                name: "Auth1stt",
                table: "Customers",
                newName: "Auth2ndDt");

            migrationBuilder.AddColumn<DateTime>(
                name: "Auth1stDt",
                table: "Customers",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MakeBy",
                table: "Customers",
                type: "NVARCHAR2(2000)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth1stDt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MakeBy",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "MakeDt",
                table: "Customers",
                newName: "Auth2ndAt");

            migrationBuilder.RenameColumn(
                name: "Auth2ndDt",
                table: "Customers",
                newName: "Auth1stt");
        }
    }
}
