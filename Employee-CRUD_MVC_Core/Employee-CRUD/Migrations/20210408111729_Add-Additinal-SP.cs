using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_CRUD.Migrations
{
    public partial class AddAdditinalSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string DeletePublicPost = @"
                CREATE PROCEDURE [dbo].[PR_POST_PublicPost_Delete]
	                @PostID		int
                AS 
                Begin
                    DELETE FROM [dbo].[TBL_PublicPost]
                    WHERE	[dbo].[TBL_PublicPost].[PostID] = @PostID
                End";

            string InsertPublicPost = @"
                CREATE PROCEDURE [dbo].[PR_POST_PublicPost_Insert]
			                 @PostID						int     OUTPUT
			                ,@Tags				nvarchar(500)
			                ,@ImageName				nvarchar(100)
			                ,@UserID				int
			                ,@CategoryID			int
			                ,@PostedDateTime			datetime
                AS 
                BEGIN
                BEGIN TRY
                BEGIN TRAN
                INSERT INTO [dbo].[TBL_PublicPost]
			                (
			                 [dbo].[TBL_PublicPost].[Tags]
			                ,[dbo].[TBL_PublicPost].[ImageName]
			                ,[dbo].[TBL_PublicPost].[UserID]
			                ,[dbo].[TBL_PublicPost].[CategoryID]
			                ,[dbo].[TBL_PublicPost].[PostedDateTime]
			                )
	                VALUES
			                (
			                 @Tags
			                ,@ImageName
			                ,@UserID
			                ,@CategoryID
			                ,@PostedDateTime
			                )
                SET @PostID = @@IDENTITY
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

            string UpdatePublicPost = @"
                CREATE PROCEDURE [dbo].[PR_POST_PublicPost_Update]
			                 @PostID			    int
			                ,@Tags				    nvarchar(500)
			                ,@ImageName				nvarchar(100)
			                ,@UserID				int
			                ,@CategoryID			int
			                ,@PostedDateTime		datetime
                AS 
                BEGIN
                BEGIN TRY
                BEGIN TRAN
                UPDATE  [dbo].[TBL_PublicPost] SET
			                 [dbo].[TBL_PublicPost].[Tags] = @Tags
			                ,[dbo].[TBL_PublicPost].[ImageName] = @ImageName
			                ,[dbo].[TBL_PublicPost].[UserID] = @UserID
			                ,[dbo].[TBL_PublicPost].[CategoryID] = @CategoryID
			                ,[dbo].[TBL_PublicPost].[PostedDateTime] = @PostedDateTime
			                
                WHERE	[dbo].[TBL_PublicPost].[PostID] = @PostID
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

            migrationBuilder.Sql(DeletePublicPost);
            migrationBuilder.Sql(InsertPublicPost);
            migrationBuilder.Sql(UpdatePublicPost);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
