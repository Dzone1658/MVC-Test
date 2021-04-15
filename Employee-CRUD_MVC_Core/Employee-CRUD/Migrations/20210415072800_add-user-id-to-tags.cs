using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_CRUD.Migrations
{
    public partial class adduseridtotags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "TBL_PostTags",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "TBL_PostTags");
        }
    }
}
