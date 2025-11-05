using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.Migrations
{
    /// <inheritdoc />
    public partial class editTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actorCategories_Actors_ActorId",
                table: "actorCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_actorCategories_Categories_CategoryId",
                table: "actorCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_actorCategories",
                table: "actorCategories");

            migrationBuilder.RenameTable(
                name: "actorCategories",
                newName: "ActorCategory");

            migrationBuilder.RenameIndex(
                name: "IX_actorCategories_CategoryId",
                table: "ActorCategory",
                newName: "IX_ActorCategory_CategoryId");

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Movies",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorCategory",
                table: "ActorCategory",
                columns: new[] { "ActorId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ActorCategory_Actors_ActorId",
                table: "ActorCategory",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorCategory_Categories_CategoryId",
                table: "ActorCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorCategory_Actors_ActorId",
                table: "ActorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorCategory_Categories_CategoryId",
                table: "ActorCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorCategory",
                table: "ActorCategory");

            migrationBuilder.RenameTable(
                name: "ActorCategory",
                newName: "actorCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ActorCategory_CategoryId",
                table: "actorCategories",
                newName: "IX_actorCategories_CategoryId");

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Movies",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_actorCategories",
                table: "actorCategories",
                columns: new[] { "ActorId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_actorCategories_Actors_ActorId",
                table: "actorCategories",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_actorCategories_Categories_CategoryId",
                table: "actorCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
