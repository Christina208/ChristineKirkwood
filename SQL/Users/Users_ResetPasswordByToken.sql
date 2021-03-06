USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[Users_ResetPasswordByToken]    Script Date: 10/16/2020 2:39:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Users_ResetPasswordByToken] 
					@UserToken NVARCHAR(200),
					@Password varchar(100)
AS

/*----TEST CODE---- 

	DECLARE @_userToken nvarchar(200) = 'e9969f13-39e4-4450-a6c5-300eda92369b'
		 , @Password varchar(100) = 'Pass987!!'

	EXEC [dbo].[Users_ResetPasswordByToken]
			@_userToken,
			@Password 
	SELECT*
	FROM [dbo].[Users]

	SELECT * 
	FROM dbo.UserTokens
*/

    BEGIN
        SET XACT_ABORT ON;
        DECLARE @Tran NVARCHAR(50)= '[dbo].[Users_ResetPassword]_Tran';
        BEGIN TRY
            BEGIN TRANSACTION @Tran;
			DECLARE @Id INT;
			DECLARE @Token nvarchar(200);
            SELECT @Id = UserId
            FROM [dbo].[UserTokens]
            WHERE [Token] = @UserToken;

            EXEC [dbo].[Users_UpdatePassword] 
                 @Password, 
				 @Id;
            EXEC [dbo].[UserTokens_DeleteById] 
                 @Id;
            COMMIT TRANSACTION @Tran;
        END TRY
        BEGIN CATCH
            IF(XACT_STATE()) = -1
                BEGIN
                    PRINT 'The transaction is in an uncommittable state.' + ' Rolling back transaction.';
                    ROLLBACK TRANSACTION @Tran;
            END;

            -- Test whether the transaction is active and valid.
            IF(XACT_STATE()) = 1
                BEGIN
                    PRINT 'The transaction is committable.' + ' Committing transaction.';
                    COMMIT TRANSACTION @Tran;
            END;

            -- If you want to see error info
            -- SELECT
            --ERROR_NUMBER() AS ErrorNumber,
            --ERROR_SEVERITY() AS ErrorSeverity,
            --ERROR_STATE() AS ErrorState,
            -- ERROR_PROCEDURE() AS ErrorProcedure,
            -- ERROR_LINE() AS ErrorLine,
            -- ERROR_MESSAGE() AS ErrorMessage
            -- to just get the error thrown and see the bad news as an exception
            THROW;
        END CATCH;
        SET XACT_ABORT OFF;
    END;