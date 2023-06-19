using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PictureNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Picture",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "IMG_1213-2.jpg");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "10633023.jpg");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "19628054_500_A.jpg");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "59190403.jpg");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "0_sportswear-club-men-s-t-shirt-1200x1200.jpg");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "galaxy-s23-ultra-highlights-colors-green-back-s.jpg");

            migrationBuilder.UpdateData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "shelter-dog-cropped.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Picture");
        }
    }
}
