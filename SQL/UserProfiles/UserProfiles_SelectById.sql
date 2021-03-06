USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_SelectById]    Script Date: 10/16/2020 2:36:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[UserProfiles_SelectById]
	@Id INT
AS
/*
	DECLARE @_id INT = 21;
	EXEC dbo.UserProfiles_SelectById @_id;
*/
BEGIN
	SELECT 
		Id,  
		UserId,
		FirstName,
		LastName,
		Mi,
		AvatarUrl
	  FROM
		dbo.UserProfiles
	 WHERE
		Id = @Id;
END