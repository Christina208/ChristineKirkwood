USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[Users_SelectEmailById]    Script Date: 10/16/2020 2:40:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Users_SelectEmailById]

		@Email NVARCHAR(100),
		@Token VARCHAR(200),
		@IsVerified bit OUTPUt
AS

/*********************** TEST **************


Declare @Email NVARCHAR(100) = 'gill@gmail.com',
			@Token NVARCHAR(200) = 'jhgjdhsaA-sASdj-ASdsa-asd2ddsdd', 
			@IsVerified bit

EXECUTE dbo.Users_SelectEmailById  @Email,
									@Token,
									@IsVerified OUT
	SELECT @IsVerified


**********************************************/
BEGIN 

	Declare @UserId INT,
			@TokenType INT = 2
		

	SELECT @UserId = Id 
			FROM dbo.Users 
			WHERE Email = @Email;

	IF @UserId IS NOT NULL
		BEGIN
		
			INSERT INTO dbo.UserTokens 
							(UserId, 
							Token,
							TokenType)
					VALUES (@UserId, 
							@Token, 
							@TokenType)
			SET @IsVerified = 1
		END
	ELSE 
		BEGIN
			SET @IsVerified = 0
		END


END