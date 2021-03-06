USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserTokens_InsertByEmail]    Script Date: 10/16/2020 2:42:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[UserTokens_InsertByEmail] @UserToken NVARCHAR(200), 
                                            @Email     NVARCHAR(100), 
                                            @TokenType INT
AS

/*
		---should check email then insert token 

Declare @_UserToken nvarchar(200) = 'test token123'
		, @_Email nvarchar(100) = 'ray2@gmail.com'
		, @_TokenType int = 2

Execute [dbo].[UserTokens_InsertByEmail]
			@_UserToken
			, @_Email
			, @_TokenType

*/

    BEGIN
        SET XACT_ABORT ON;
        DECLARE @Tran NVARCHAR(50)= '[dbo].[UserTokens_InsertByEmail]_Tran';
        BEGIN TRY
            BEGIN TRANSACTION @Tran;
            DECLARE @UserId INT;
            SELECT @UserId = Id
            FROM [dbo].[Users]
            WHERE Email = @Email;
            INSERT INTO [dbo].[UserTokens]
            (Token, 
             UserId, 
             TokenType
            )
            VALUES
            (@UserToken, 
             @UserId, 
             @TokenType
            );
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
            --SELECT
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