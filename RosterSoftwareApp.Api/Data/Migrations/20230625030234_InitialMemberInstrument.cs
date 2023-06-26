using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterSoftwareApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMemberInstrument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberInstruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberInstruments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberInstruments");
        }
    }
}
