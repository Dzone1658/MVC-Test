using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_CRUD.Migrations
{
    public partial class TagSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string GetTag = @"
                CREATE PROCEDURE [dbo].[PR_GET_Tag]
	                   
                    AS
                    BEGIN
                        SELECT * FROM [dbo].[TBL_Tags] WHERE IsACtive = 1 
                    END";

            string InsertTag = @"
                CREATE PROCEDURE [dbo].[PR_POST_Tag_Insert]
			                @TagID			int
			                @TagName		nvarchar(100)
                            @IsActive       bit = 1
                AS 
                BEGIN
                BEGIN TRY
                BEGIN TRAN
                INSERT INTO [dbo].[TBL_Tags]
			                (
			                [dbo].[TBL_Tags].[TagName]
			                ,[dbo].[TBL_Tags].[IsActive]
			                )
	                VALUES
			                (
			                @TagName
			                ,@IsActive
			                )
                SET @TagId = @@IDENTITY
                COMMIT TRAN
                END TRY
                BEGIN CATCH
	                SELECT   
                        ERROR_NUMBER() AS ErrorNumber  
                        ,ERROR_SEVERITY() AS ErrorSeverity  
                        ,ERROR_STATE() AS ErrorState  
                        ,ERROR_PROCEDURE() AS ErrorProcedure  
                        ,ERROR_LINE() AS ErrorLine  
                        ,ERROR_MESSAGE() AS ErrorMessage;  
                    IF(@@TRANCOUNT > 0)
                        ROLLBACK TRAN;
                    THROW;  
                END CATCH
                END";

            string GetTagById = @"
                    CREATE PROCEDURE [dbo].[PR_GET_Tag_By_User&Post_Id]
                        @UserId     nvarchar(100) = NULL,
                        @PostId     int = 0
                    AS
                    BEGIN
                        IF(@UserId IS NOT NULL)
                            SELECT TBL_PostTags.PostID, TBL_PostTags.TagID, TBL_Tags.TagName, TBL_PostTags.UserID
                            FROM TBL_PostTags, TBL_Tags
                            WHERE TBL_PostTags.UserID = @UserId AND TBL_PostTags.IsActive = 1
                        ELSE IF(@PostId > 0)
                            SELECT TBL_PostTags.PostID, TBL_PostTags.TagID, TBL_Tags.TagName, TBL_PostTags.UserID
                            FROM TBL_PostTags, TBL_Tags
                            WHERE TBL_PostTags.PostID = @PostId AND TBL_PostTags.IsActive = 1
                    END";

            migrationBuilder.Sql(GetTag);
            migrationBuilder.Sql(GetTagById);
            migrationBuilder.Sql(InsertTag);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
