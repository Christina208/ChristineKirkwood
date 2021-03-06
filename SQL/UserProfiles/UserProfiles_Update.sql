USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Update]    Script Date: 10/16/2020 2:37:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UserProfiles_Update]
	@Id INT,
	@FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
	@Mi NVARCHAR(2),
	@AvatarUrl VARCHAR(255)
AS
/*
	DECLARE
		@_id INT,
		@_firstName NVARCHAR(100) = 'M',
		@_lastName NVARCHAR(100) = 'Mouse',
		@_mi NVARCHAR(2) = '1970-05-06',
		@_avatarUrl VARCHAR(255) = 'M';
	
	EXEC dbo.UserProfiles_Insert
		@_id OUT,
		@_firstName,
		@_lastName,
		@_mi,
		@_avatarUrl;
		
	SELECT * FROM dbo.UserProfiles WHERE Id = @_id;
*/


BEGIN
	DECLARE @ModifiedDate DATE = getutcdate()
	UPDATE
		dbo.UserProfiles
	SET

		FirstName = @FirstName,
		LastName = @LastName,
		Mi = @Mi,
		AvatarUrl = @AvatarUrl
	WHERE
		Id = @Id;
END