using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Pictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Bulletins");

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BulletinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Bulletins_BulletinId",
                        column: x => x.BulletinId,
                        principalTable: "Bulletins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_BulletinId",
                table: "Picture",
                column: "BulletinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "Bulletins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Bulletins",
                columns: new[] { "Id", "BulletinType", "CategoryId", "CityId", "Contacts", "Description", "OwnerId", "Pictures", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Call me +12387298283", null, null, "[\"https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg\"]", 650m, "iPhone X" },
                    { 2, 1, 2, 1, "Call me +38558598988", null, null, "[\"https://content2.rozetka.com.ua/goods/images/big/10633023.jpg\"]", 650m, "PowerBall" },
                    { 3, 1, 3, 3, "Text me @shirtseller", null, null, "[\"https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg\",\"http://brands-online.com.ua/images/products/view/59190403.jpg\",\"https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg\"]", 650m, "Nike T-Shirt" },
                    { 4, 1, 1, 2, "Message me @phoneseller for more info", null, null, "[\"https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg\"]", 650m, "Samsung S23" },
                    { 5, 2, 9, 2, "Paw shelter at Independency st, Kyiv", null, null, "[\"https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg\"]", 0m, "A doggo looks for new owner" }
                });
        }
    }
}
