using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class one_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultipleImages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assignmentsid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_MultipleImages_Assignments_Assignmentsid",
                        column: x => x.Assignmentsid,
                        principalTable: "Assignments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultipleImages_Assignmentsid",
                table: "MultipleImages",
                column: "Assignmentsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleImages");
        }
    }
}
