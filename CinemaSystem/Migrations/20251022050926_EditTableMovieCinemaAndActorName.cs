using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.Migrations
{
    /// <inheritdoc />
    public partial class EditTableMovieCinemaAndActorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorCategory_Actors_ActorId",
                table: "ActorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorCategory_Categories_CategoryId",
                table: "ActorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialLink_Actors_ActorId",
                table: "SocialLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialLink",
                table: "SocialLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorCategory",
                table: "ActorCategory");

            migrationBuilder.RenameTable(
                name: "SocialLink",
                newName: "socialLinks");

            migrationBuilder.RenameTable(
                name: "ActorCategory",
                newName: "actorCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ActorCategory_CategoryId",
                table: "actorCategories",
                newName: "IX_actorCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_socialLinks",
                table: "socialLinks",
                columns: new[] { "ActorId", "Platform" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_socialLinks_Actors_ActorId",
                table: "socialLinks",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actorCategories_Actors_ActorId",
                table: "actorCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_actorCategories_Categories_CategoryId",
                table: "actorCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_socialLinks_Actors_ActorId",
                table: "socialLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_socialLinks",
                table: "socialLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_actorCategories",
                table: "actorCategories");

            migrationBuilder.RenameTable(
                name: "socialLinks",
                newName: "SocialLink");

            migrationBuilder.RenameTable(
                name: "actorCategories",
                newName: "ActorCategory");

            migrationBuilder.RenameIndex(
                name: "IX_actorCategories_CategoryId",
                table: "ActorCategory",
                newName: "IX_ActorCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialLink",
                table: "SocialLink",
                columns: new[] { "ActorId", "Platform" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_SocialLink_Actors_ActorId",
                table: "SocialLink",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
