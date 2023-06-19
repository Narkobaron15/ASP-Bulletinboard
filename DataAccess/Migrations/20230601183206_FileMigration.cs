using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BulletinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_Bulletins_BulletinId",
                        column: x => x.BulletinId,
                        principalTable: "Bulletins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "File",
                columns: new[] { "Id", "BulletinId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, 1, "IMG_1213-2.jpg", "https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg" },
                    { 2, 2, "10633023.jpg", "https://content2.rozetka.com.ua/goods/images/big/10633023.jpg" },
                    { 3, 3, "19628054_500_A.jpg", "https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg" },
                    { 4, 3, "59190403.jpg", "http://brands-online.com.ua/images/products/view/59190403.jpg" },
                    { 5, 3, "0_sportswear-club-men-s-t-shirt-1200x1200.jpg", "https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg" },
                    { 6, 4, "galaxy-s23-ultra-highlights-colors-green-back-s.jpg", "https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg" },
                    { 7, 5, "shelter-dog-cropped.jpg", "https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_BulletinId",
                table: "File",
                column: "BulletinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BulletinId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "BulletinId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, 1, "IMG_1213-2.jpg", "https://tvoygadget.com.ua/wp-content/uploads/2019/01/IMG_1213-2.jpg" },
                    { 2, 2, "10633023.jpg", "https://content2.rozetka.com.ua/goods/images/big/10633023.jpg" },
                    { 3, 3, "19628054_500_A.jpg", "https://photos6.spartoo.com/photos/196/19628054/19628054_500_A.jpg" },
                    { 4, 3, "59190403.jpg", "http://brands-online.com.ua/images/products/view/59190403.jpg" },
                    { 5, 3, "0_sportswear-club-men-s-t-shirt-1200x1200.jpg", "https://fair.ua/image/cache/catalog/photo_prod/11458682/0_sportswear-club-men-s-t-shirt-1200x1200.jpg" },
                    { 6, 4, "galaxy-s23-ultra-highlights-colors-green-back-s.jpg", "https://images.samsung.com/ua/smartphones/galaxy-s23-ultra/images/galaxy-s23-ultra-highlights-colors-green-back-s.jpg" },
                    { 7, 5, "shelter-dog-cropped.jpg", "https://www.petfinder.com/sites/default/files/images/content/shelter-dog-cropped.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_BulletinId",
                table: "Picture",
                column: "BulletinId");
        }
    }
}
