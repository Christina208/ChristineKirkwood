USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[Users_UpdatePasswordByToken]    Script Date: 10/16/2020 2:41:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Users_UpdatePasswordByToken]
	@Password varchar(100)
	,@Id nvarchar(200)


as

/*
--------------Test Code----------Last Modified By----Gill Chang----08-17-20 @ 10:31 PST----

Declare	@Password varchar(100) = "password"
Declare	@Id int = 1

Execute dbo.Users_UpdatePasswordByToken
		@Password
		,@Id



Select @Id

Select *
	From dbo.Users
	Where Id = @Id

*/
Begin
Declare @DateModified datetime2(7) = getutcdate()
UPDATE [dbo].[Users]
   SET [Password] = @Password
      ,[DateModified] = @DateModified

 WHERE Id = @Id

END