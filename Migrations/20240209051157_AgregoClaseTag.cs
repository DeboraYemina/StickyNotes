using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotasApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregoClaseTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tagId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_tagId",
                table: "Notes",
                column: "tagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tags_tagId",
                table: "Notes",
                column: "tagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tags_tagId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Notes_tagId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "tagId",
                table: "Notes");
        }
    }
}
