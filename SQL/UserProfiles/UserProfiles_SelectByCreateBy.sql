USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_SelectByCreateBy]    Script Date: 10/16/2020 2:34:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER PROC [dbo].[UserProfiles_SelectByCreateBy]    
 @userId int
 ,@pageIndex int 
 ,@pageSize int


AS
/*
Declare @userId int = 2

Declare @PageIndex int = 0
		,@PageSize int = 2

Execute dbo.UserProfiles_SelectByCreateBy  @userId
											,@PageIndex
											,@PageSize
*/
BEGIN

   Declare @offset int = @pageIndex * @pageSize
      
        SELECT   Id
				, UserId
				, FirstName
				, LastName
				, Mi
				, AvatarUrl

                , TotalCount = COUNT(1) OVER()
        FROM    dbo.UserProfiles 
        
		WHERE UserId = @userId
		
		ORDER BY Id

	OFFSET @offSet Rows
	Fetch Next @pageSize Rows ONLY

END