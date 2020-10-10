using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentSearch.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentListings_Categories_CartegoryId",
                table: "ApartmentListings");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentListings_CartegoryId",
                table: "ApartmentListings");

            migrationBuilder.DropColumn(
                name: "CartegoryId",
                table: "ApartmentListings");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ApartmentListings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentListings_CategoryId",
                table: "ApartmentListings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentListings_Categories_CategoryId",
                table: "ApartmentListings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentListings_Categories_CategoryId",
                table: "ApartmentListings");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentListings_CategoryId",
                table: "ApartmentListings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ApartmentListings");

            migrationBuilder.AddColumn<int>(
                name: "CartegoryId",
                table: "ApartmentListings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentListings_CartegoryId",
                table: "ApartmentListings",
                column: "CartegoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentListings_Categories_CartegoryId",
                table: "ApartmentListings",
                column: "CartegoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
