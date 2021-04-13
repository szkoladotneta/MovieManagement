using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieManagement.Persistance.Migrations
{
    public partial class configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DirectorName_LastName",
                table: "Directors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "DirectorName_FirstName",
                table: "Directors",
                newName: "FirstName");

            migrationBuilder.AlterColumn<int>(
                name: "PremiereYear",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 2000,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 4, 2, 0, 49, 10, 642, DateTimeKind.Local).AddTicks(7446));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Directors",
                newName: "DirectorName_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Directors",
                newName: "DirectorName_FirstName");

            migrationBuilder.AlterColumn<int>(
                name: "PremiereYear",
                table: "Movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 4, 2, 0, 22, 9, 928, DateTimeKind.Local).AddTicks(352));
        }
    }
}
