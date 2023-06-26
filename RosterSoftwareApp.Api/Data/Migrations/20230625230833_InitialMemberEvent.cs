using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterSoftwareApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMemberEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Confirm = table.Column<bool>(type: "bit", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    MemberInstrumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberEvents_MemberInstruments_MemberInstrumentId",
                        column: x => x.MemberInstrumentId,
                        principalTable: "MemberInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberEvents_EventId",
                table: "MemberEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEvents_MemberInstrumentId",
                table: "MemberEvents",
                column: "MemberInstrumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberEvents");
        }
    }
}
