using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetalCollector.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ArtistId",
                table: "Items",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Artists_ArtistId",
                table: "Items",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Artists_ArtistId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ArtistId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
