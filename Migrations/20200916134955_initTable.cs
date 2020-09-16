using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace demo1.Migrations
{
    public partial class initTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaypalToken",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    scope = table.Column<string>(nullable: true),
                    token_type = table.Column<string>(nullable: true),
                    app_id = table.Column<string>(nullable: true),
                    access_token = table.Column<string>(nullable: true),
                    expires_in = table.Column<int>(nullable: false),
                    nonce = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaypalToken", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaypalToken");
        }
    }
}
