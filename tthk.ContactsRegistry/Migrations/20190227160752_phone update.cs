using Microsoft.EntityFrameworkCore.Migrations;

namespace tthk.ContactsRegistry.Migrations
{
    public partial class phoneupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumbers",
                table: "ContactEasies",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ContactEasies",
                newName: "PhoneNumbers");
        }
    }
}
