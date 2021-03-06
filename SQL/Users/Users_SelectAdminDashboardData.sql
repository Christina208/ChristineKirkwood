USE [C90Machitia]
GO
/****** Object:  StoredProcedure [dbo].[Users_SelectAdminDashboardData]    Script Date: 10/16/2020 2:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Users_SelectAdminDashboardData]
AS

/*
	execute [dbo].[Users_SelectAdminDashboardData]
	
	*/

    BEGIN
        DECLARE @table TABLE
        (total INT, 
         Id    INT
        );
        INSERT INTO @table
               SELECT TOP 4 COUNT(PlanId) AS PlanCount, 
                            PlanId
               FROM dbo.PlanFavorite
               GROUP BY PlanId
               ORDER BY 1 DESC;

			    DECLARE @grouptable TABLE
        (total INT, 
         Id    INT
        );
        INSERT INTO @grouptable
               SELECT TOP 4 COUNT(GroupId) AS GroupCount, 
                            GroupId
               FROM dbo.GroupFollowers
               GROUP BY GroupId
               ORDER BY 1 DESC;
        SELECT
        (
            SELECT COUNT(*)
            FROM dbo.Users
            WHERE [isConfirmed] = 1
                  AND [UserStatusId] = 1
        ) AS ActiveUser, 
        (
            SELECT COUNT(*)
            FROM [dbo].[Group]
            WHERE [isActive] = 1
        ) AS ActiveGroup, 
        (
            SELECT COUNT(*)
            FROM dbo.Organizations
        ) AS Organizations, 
        (
            SELECT COUNT(*)
            FROM [dbo].[Plan]
        ) AS TotalPlans, 
        (
            SELECT p.id, 
                   p.title, 
                   p.[Subject], 
				   p.Overview, 
				   p.duration, 
				   p.CoverImageUrl, 
				   p.CreatedBy
            FROM dbo.[Plan] p
                 JOIN @table t ON p.Id = t.Id FOR JSON AUTO
        ) AS TopPlans, 
        (
            SELECT g.id, g.[name],g.ImageUrl, g.[Description], g.IsActive, g.IsPrivate, g.CreatedBy
            FROM dbo.[Group] g
			Where g.Id IN  (SElect Id from @grouptable) FOR JSON AUTO
        ) AS MostFollowedGroups, 
        (
            SELECT TOP 4 U.Id as UserId,UP.FirstName, UP.LastName, Up.Mi, UP.AvatarUrl 
            FROM dbo.Users as U 
			join dbo.UserProfiles as UP on UP.UserId = u.Id
            WHERE [isConfirmed] = 1
                  AND [UserStatusId] = 1
            ORDER BY U.DateCreated DESC 
			FOR JSON PATH
        ) AS RecentlyAdded;
    END;