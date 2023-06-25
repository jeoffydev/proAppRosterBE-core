using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterSoftwareApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdatememberInsRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MemberInstruments_InstrumentId",
                table: "MemberInstruments",
                column: "InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberInstruments_Instruments_InstrumentId",
                table: "MemberInstruments",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberInstruments_Instruments_InstrumentId",
                table: "MemberInstruments");

            migrationBuilder.DropIndex(
                name: "IX_MemberInstruments_InstrumentId",
                table: "MemberInstruments");
        }
    }
}
