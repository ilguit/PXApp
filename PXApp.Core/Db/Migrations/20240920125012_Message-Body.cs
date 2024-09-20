using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PXApp.Core.Db.Migrations
{
    /// <inheritdoc />
    public partial class MessageBody : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "message",
                table: "messages",
                newName: "body");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "body",
                table: "messages",
                newName: "message");
        }
    }
}
