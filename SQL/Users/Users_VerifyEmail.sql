USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[Users_VerifyEmail]    Script Date: 10/16/2020 2:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Users_VerifyEmail]
	@Email NVARCHAR(100)
AS
/*
	DECLARE @_email NVARCHAR(100) = 'c@jon.com';
	EXEC dbo.Users_VerifyEmail @_email;
*/
BEGIN
	SELECT   
		Email
		
	  FROM
		dbo.Users
	 WHERE
		[Email] = @Email;
END