using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_CRUD.Migrations
{
    public partial class GetPublicPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string GetPosts = @"
                CREATE PROCEDURE [dbo].[PR_GET_PublicPost]
	                @UserID	 nvarchar(100) = NULL,
                    @IsActive = 1
                AS 
                BEGIN
                    
                    IF(@UserId IS NOT NULL)
                        SELECT * FROM  [dbo].[TBL_PublicPost]
                        WHERE [dbo].[TBL_PublicPost].[UserId] = @UserId
                            AND [dbo].[TBL_PublicPost].[IsActive] = 1
                    ELSE
                        SELECT * FROM [dbo].[TBL_PublicPost]
                END";
            
            string GetCategory = @"
                CREATE PROCEDURE [dbo].[PR_GET_Categories]
	                @IsActive = 1
                AS 
                BEGIN
                    SELECT * FROM [dbo].[TBL_Category] 
                    WHERE IsActive = 1
                END";

            migrationBuilder.Sql(GetPosts);
            migrationBuilder.Sql(GetCategory);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
