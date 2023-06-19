using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BulletinOwnership1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pictures",
                value: "[\"https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 2,
                column: "Pictures",
                value: "[\"https://content2.rozetka.com.ua/goods/images/big/10633023.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 3,
                column: "Pictures",
                value: "[\"https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg\",\"http://brands-online.com.ua/images/products/view/59190403.jpg\",\"https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 4,
                column: "Pictures",
                value: "[\"https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 5,
                column: "Pictures",
                value: "[\"https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg\"]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pictures",
                value: "[\"\",\"https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 2,
                column: "Pictures",
                value: "[\"\",\"https://content2.rozetka.com.ua/goods/images/big/10633023.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 3,
                column: "Pictures",
                value: "[\"\",\"https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg\",\"http://brands-online.com.ua/images/products/view/59190403.jpg\",\"https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 4,
                column: "Pictures",
                value: "[\"\",\"https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg\"]");

            migrationBuilder.UpdateData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 5,
                column: "Pictures",
                value: "[\"\",\"https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg\"]");
        }
    }
}
