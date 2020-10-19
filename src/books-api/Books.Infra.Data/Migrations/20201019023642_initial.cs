using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Infra.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Profile = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteBook",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookId = table.Column<string>(maxLength: 50, nullable: false),
                    Link = table.Column<string>(maxLength: 1000, nullable: false),
                    Title = table.Column<string>(maxLength: 400, nullable: false),
                    Thumbnail = table.Column<string>(maxLength: 1000, nullable: true),
                    Description = table.Column<string>(maxLength: 8000, nullable: true),
                    Authors = table.Column<string>(maxLength: 1000, nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteBook_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteBook_UserId",
                table: "FavoriteBook",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteBook");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
