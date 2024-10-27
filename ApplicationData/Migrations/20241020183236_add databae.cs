using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationData.Migrations
{
    /// <inheritdoc />
    public partial class adddatabae : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Visitors_VisitorId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "ExhibitionId",
                table: "Visitors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExhibitionId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VisitorId1",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_VisitorId1",
                table: "Activities",
                column: "VisitorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Visitors_VisitorId1",
                table: "Activities",
                column: "VisitorId1",
                principalTable: "Visitors",
                principalColumn: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Visitors_VisitorId",
                table: "Reservations",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Visitors_VisitorId1",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Visitors_VisitorId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Activities_VisitorId1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "VisitorId1",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "ExhibitionId",
                table: "Visitors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExhibitionId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Visitors_VisitorId",
                table: "Reservations",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId");
        }
    }
}
