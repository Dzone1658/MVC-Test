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

            string getcategory = @"
                create procedure [dbo].[pr_get_categories]
	                @isactive = 1
                as 
                begin
                    select * from [dbo].[tbl_category] 
                    where isactive = 1
                end";

            migrationBuilder.Sql(GetPosts);
            migrationBuilder.Sql(getcategory);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
