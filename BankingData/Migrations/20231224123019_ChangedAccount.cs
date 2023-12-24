using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingData.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHalt",
                table: "Accounts",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Accounts",
                newName: "PasswordHalt");
        }
    }
}
