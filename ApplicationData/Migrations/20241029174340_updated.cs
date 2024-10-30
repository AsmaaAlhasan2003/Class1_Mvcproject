using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationData.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Exhibitions_ExhibitionId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Visitors_VisitorId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Exhibitions_ExhibitionId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_ExhibitionId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Loans_VisitorId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Books_ExhibitionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ExhibitionId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ExhibitionId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Discraption",
                table: "Exhibitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discraption",
                table: "Exhibitions");

            migrationBuilder.AddColumn<int>(
                name: "ExhibitionId",
                table: "Visitors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitorId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ExhibitionId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_ExhibitionId",
                table: "Visitors",
                column: "ExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_VisitorId",
                table: "Loans",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ExhibitionId",
                table: "Books",
                column: "ExhibitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Exhibitions_ExhibitionId",
                table: "Books",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Exhibitionid");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Visitors_VisitorId",
                table: "Loans",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Exhibitions_ExhibitionId",
                table: "Visitors",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Exhibitionid");
        }
    }
}
