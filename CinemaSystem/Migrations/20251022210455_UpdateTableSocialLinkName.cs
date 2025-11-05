using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableSocialLinkName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_socialLinks_Actors_ActorId",
                table: "socialLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_socialLinks",
                table: "socialLinks");

            migrationBuilder.RenameTable(
                name: "socialLinks",
                newName: "SocialLink");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialLink",
                table: "SocialLink",
                columns: new[] { "ActorId", "Platform" });

            migrationBuilder.AddForeignKey(
                name: "FK_SocialLink_Actors_ActorId",
                table: "SocialLink",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialLink_Actors_ActorId",
                table: "SocialLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialLink",
                table: "SocialLink");

            migrationBuilder.RenameTable(
                name: "SocialLink",
                newName: "socialLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_socialLinks",
                table: "socialLinks",
                columns: new[] { "ActorId", "Platform" });

            migrationBuilder.AddForeignKey(
                name: "FK_socialLinks_Actors_ActorId",
                table: "socialLinks",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
