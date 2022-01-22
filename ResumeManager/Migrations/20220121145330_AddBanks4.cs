using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeManager.Migrations
{
    public partial class AddBanks4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BankID",
                table: "Banks",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Banks",
                newName: "BankID");
        }
    }
}
