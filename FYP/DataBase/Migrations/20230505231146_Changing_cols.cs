using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Changing_cols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegNo",
                table: "signUps",
                newName: "DOB");

            migrationBuilder.RenameColumn(
                name: "RegNo",
                table: "SignIns",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Registration_No",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "AspNetUsers",
                newName: "DOB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "signUps",
                newName: "RegNo");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SignIns",
                newName: "RegNo");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Registration_No");

            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "AspNetUsers",
                newName: "DateTime");
        }
    }
}
