using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterSoftwareApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedYoutubeEmbedToSongs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YoutubeEmbed",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeEmbed",
                table: "Songs");
        }
    }
}
