using Microsoft.EntityFrameworkCore.Migrations;

namespace FloCares.Data.Migrations
{
    public partial class bedcategoryupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BedCategory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BedCategory");
        }
    }
}
