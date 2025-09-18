using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelProject.DataAccessLayer.Migrations
{
    public partial class delete_MessageCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_MessageCategories_MessageCategoryID",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_MessageCategoryID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MessageCategoryID",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageCategoryID",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_MessageCategoryID",
                table: "Contacts",
                column: "MessageCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_MessageCategories_MessageCategoryID",
                table: "Contacts",
                column: "MessageCategoryID",
                principalTable: "MessageCategories",
                principalColumn: "MessageCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
