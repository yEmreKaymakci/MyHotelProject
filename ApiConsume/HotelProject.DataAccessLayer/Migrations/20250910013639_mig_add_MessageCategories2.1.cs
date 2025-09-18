using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelProject.DataAccessLayer.Migrations
{
    public partial class mig_add_MessageCategories21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MessageCategories",
                keyColumn: "MessageCategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MessageCategories",
                keyColumn: "MessageCategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MessageCategories",
                keyColumn: "MessageCategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MessageCategories",
                keyColumn: "MessageCategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MessageCategories",
                keyColumn: "MessageCategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MessageCategories",
                keyColumn: "MessageCategoryID",
                keyValue: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MessageCategories",
                columns: new[] { "MessageCategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Rezervasyon" },
                    { 2, "Şikayet" },
                    { 3, "Öneri" },
                    { 4, "Teşekkür" },
                    { 5, "Bilgi Talebi" },
                    { 6, "Diğer" }
                });
        }
    }
}
