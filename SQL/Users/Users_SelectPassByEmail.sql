USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[Users_SelectPassByEmail]    Script Date: 10/16/2020 2:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Users_SelectPassByEmail]
	@Email NVARCHAR(100)
AS
/*
	DECLARE @_email NVARCHAR(100) = 'jon@jon.com';
	EXEC dbo.Users_SelectPassByEmail @_email;
*/
BEGIN
	SELECT   
		[Password]
		
	  FROM
		dbo.Users
	 WHERE
		[Email] = @Email;
END