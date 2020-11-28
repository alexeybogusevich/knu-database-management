using Microsoft.EntityFrameworkCore.Migrations;

namespace KNU.IT.DbManager.Migrations
{
    public partial class RowNameDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Row");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Row",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
