using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class Final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Booking_booking_ID",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_booking_ID",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "booking_ID",
                table: "Promotions");

            migrationBuilder.AlterColumn<string>(
                name: "Place_Rating",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Place_Photo",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "booking_ID",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Place_Rating",
                table: "Places",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Place_Photo",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_booking_ID",
                table: "Promotions",
                column: "booking_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Booking_booking_ID",
                table: "Promotions",
                column: "booking_ID",
                principalTable: "Booking",
                principalColumn: "booking_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
