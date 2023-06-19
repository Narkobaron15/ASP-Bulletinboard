using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Pictures_WithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bulletins",
                columns: new[] { "Id", "BulletinType", "CategoryId", "CityId", "Contacts", "Description", "OwnerId", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Call me +123872982839", null, null, 650m, "iPhone X" },
                    { 2, 1, 2, 1, "Call me +385585989889", null, null, 650m, "PowerBall" },
                    { 3, 1, 3, 3, "Text me @shirt_seller", null, null, 650m, "Nike T-Shirt" },
                    { 4, 1, 1, 2, "Message me @phone_seller for more info", null, null, 650m, "Samsung Galaxy S23" },
                    { 5, 2, 9, 2, "Paw shelter at Independency st, 123, Kyiv", null, null, 0m, "A doggo looks for new owner" }
                });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "BulletinId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg" },
                    { 2, 2, "https://content2.rozetka.com.ua/goods/images/big/10633023.jpg" },
                    { 3, 3, "https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg" },
                    { 4, 3, "http://brands-online.com.ua/images/products/view/59190403.jpg" },
                    { 5, 3, "https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg" },
                    { 6, 4, "https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg" },
                    { 7, 5, "https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Picture",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bulletins",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
