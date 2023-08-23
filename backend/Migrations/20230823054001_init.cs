using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    accountnum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardnumber = table.Column<int>(type: "int", nullable: false),
                    pinnum = table.Column<int>(type: "int", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accounttype = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.accountnum);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    transactionNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<double>(type: "float", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountnum = table.Column<int>(type: "int", nullable: false),
                    recipient = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.transactionNo);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    accNo = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
