using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeManager.Migrations
{
    public partial class AddBanks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankID);
                    //table.ForeignKey(
                    //   name: "FK_Transactions_Banks_BankID",
                    //   column: x => x.BankID,
                    //   principalTable: "Banks",
                    //   principalColumn: "ID",
                    //   onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
