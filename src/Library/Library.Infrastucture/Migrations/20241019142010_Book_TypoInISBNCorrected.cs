using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class Book_TypoInISBNCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IBSN",
                table: "Books",
                newName: "ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Books",
                newName: "IBSN");
        }
    }
}
