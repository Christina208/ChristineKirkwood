USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_SelectAllPaginated]    Script Date: 10/16/2020 2:33:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER PROC [dbo].[UserProfiles_SelectAllPaginated]    
 @pageIndex int 
                                             ,@pageSize int


AS
/*
Declare @PageIndex int = 0
Declare @PageSize int = 5
Execute dbo.UserProfiles_SelectAllPaginated 
@PageIndex
,@PageSize
*/

BEGIN

   Declare @offset int = @pageIndex * @pageSize

        SELECT Id,
			   UserId,
			   FirstName, 
			   LastName, 
			   Mi, 
			   AvatarUrl,
			   TotalCount = COUNT(1) OVER() 
        FROM    dbo.UserProfiles 
        ORDER BY Id

	OFFSET @offSet Rows
	Fetch Next @pageSize Rows ONLY

END