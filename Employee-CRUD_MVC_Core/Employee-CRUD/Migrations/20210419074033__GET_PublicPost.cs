using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_CRUD.Migrations
{
    public partial class _GET_PublicPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string GetPosts = @"
                CREATE PROCEDURE [dbo].[PR_GET_PublicPost]
	                @UserID	 nvarchar(100) = NULL
                AS 
                BEGIN
                    SELECT * FROM [dbo].[TBL_PublicPost]
                    IF(@UserId is not null)
                        SELECT * FROM  [dbo].[TBL_PublicPost]
                        WHERE [dbo].[TBL_PublicPost].[UserId] = @UserId
                            AND [dbo].[TBL_PublicPost].[IsActive] = 1
                END";

            migrationBuilder.Sql(GetPosts);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
