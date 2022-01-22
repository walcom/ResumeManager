using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeManager.Migrations
{
    public partial class AddBanks30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BankID",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Banks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankID",
                table: "Transactions",
                column: "BankID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Banks_BankID",
                table: "Transactions",
                column: "BankID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Banks_BankID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankID",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "BankID",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Banks",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
