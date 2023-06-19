using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Bulletins_BulletinId",
                table: "File");

            migrationBuilder.AlterColumn<int>(
                name: "BulletinId",
                table: "File",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 1,
                column: "BulletinId",
                value: null);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 2,
                column: "BulletinId",
                value: null);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 3,
                column: "BulletinId",
                value: null);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 4,
                column: "BulletinId",
                value: null);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 5,
                column: "BulletinId",
                value: null);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 6,
                column: "BulletinId",
                value: null);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 7,
                column: "BulletinId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_File_Bulletins_BulletinId",
                table: "File",
                column: "BulletinId",
                principalTable: "Bulletins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Bulletins_BulletinId",
                table: "File");

            migrationBuilder.AlterColumn<int>(
                name: "BulletinId",
                table: "File",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 1,
                column: "BulletinId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 2,
                column: "BulletinId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 3,
                column: "BulletinId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 4,
                column: "BulletinId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 5,
                column: "BulletinId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 6,
                column: "BulletinId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "File",
                keyColumn: "Id",
                keyValue: 7,
                column: "BulletinId",
                value: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_File_Bulletins_BulletinId",
                table: "File",
                column: "BulletinId",
                principalTable: "Bulletins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
