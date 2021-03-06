USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Insert]    Script Date: 10/16/2020 2:33:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UserProfiles_Insert]
	@Id        INT OUT, 
                                      @UserId    INT, 
                                      @FirstName NVARCHAR(100), 
                                      @LastName  NVARCHAR(100), 
                                      @Mi        NVARCHAR(2), 
                                      @AvatarUrl VARCHAR(255)
AS
/*
	DECLARE
	@_id INT,
		@_userId INT = '1',
		@_firstName NVARCHAR(100) = 'Jane',
		@_lastName NVARCHAR(100) = 'Smith',
		@_Mi NVARCHAR(2) = 'S',
		@_AvatarUrl VARCHAR(255) = 'dfasdf'

	EXEC dbo.UserProfiles_Insert
		@_id OUT,
		@_userId,
		@_firstName,
		@_lastName,
		@_Mi,
		@_AvatarUrl
		
	SELECT * FROM dbo.UserProfiles WHERE Id = @_id;
*/
BEGIN
	INSERT INTO
		dbo.UserProfiles (UserId, 
         FirstName, 
         LastName, 
         Mi, 
         AvatarUrl
        ) VALUES (@UserId, 
         @FirstName, 
         @LastName, 
         @Mi, 
         @AvatarUrl
        );
	
	SET @Id = SCOPE_IDENTITY();
END

