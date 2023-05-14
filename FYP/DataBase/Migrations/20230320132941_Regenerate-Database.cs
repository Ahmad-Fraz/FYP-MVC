using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class RegenerateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberType",
                table: "signUps",
                newName: "Joinas");

            migrationBuilder.CreateTable(
                name: "SignIns",
                columns: table => new
                {
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keepMeSignedIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignIns");

            migrationBuilder.RenameColumn(
                name: "Joinas",
                table: "signUps",
                newName: "MemberType");
        }
    }
}
