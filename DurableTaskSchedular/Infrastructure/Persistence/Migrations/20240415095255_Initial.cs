using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    created_datetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_bank_accounts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scheduled_bank_account_creations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    scheduled_datetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheduled_bank_account_creations", x => x.id);
                    table.ForeignKey(
                        name: "FK_scheduled_bank_account_creations_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "scheduled_transfers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_bank_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    scheduled_datetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    bank_account_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheduled_transfers", x => x.id);
                    table.ForeignKey(
                        name: "FK_scheduled_transfers_bank_accounts_bank_account_id",
                        column: x => x.bank_account_id,
                        principalTable: "bank_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bank_accounts_user_id",
                table: "bank_accounts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_scheduled_bank_account_creations_user_id",
                table: "scheduled_bank_account_creations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_scheduled_transfers_bank_account_id",
                table: "scheduled_transfers",
                column: "bank_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scheduled_bank_account_creations");

            migrationBuilder.DropTable(
                name: "scheduled_transfers");

            migrationBuilder.DropTable(
                name: "bank_accounts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
