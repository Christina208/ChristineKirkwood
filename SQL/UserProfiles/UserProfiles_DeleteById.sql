USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_DeleteById]    Script Date: 10/16/2020 2:26:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[UserProfiles_DeleteById]
@Id int
as
/*

DECLARE @_id INT = 3;
	EXEC dbo.UserProfiles_DeleteById @_id;
*/
Begin

DELETE FROM [dbo].[UserProfiles]
      WHERE Id = @Id
End