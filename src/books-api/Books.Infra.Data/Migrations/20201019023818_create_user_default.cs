using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Infra.Data.Migrations
{
    public partial class create_user_default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [USER](ID, NAME, EMAIL, PASSWORD, PROFILE, ACTIVE) values (NEWID(), 'Admin', 'admin@mail.com', '0192023a7bbd73250516f069df18b500', 1, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM USER");
        }
    }
}
