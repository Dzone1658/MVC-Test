using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_CRUD.Migrations
{
    public partial class addsp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string QueryGet = @"
                CREATE PROCEDURE GetAllPublicPost 
                As
                Begin
                    SELECT * FROM [dbo].[TBL_PublicPost]
                End";
            migrationBuilder.Sql(QueryGet);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
