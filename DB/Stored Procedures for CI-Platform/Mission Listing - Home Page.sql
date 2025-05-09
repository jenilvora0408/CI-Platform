USE [CI_Platform]
GO
/****** Object:  StoredProcedure [dbo].[GetMissionData]    Script Date: 26-04-2023 09:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetMissionData]
--@search nvarchar(100),
@searchCountry varchar(50) = null,
@searchCity varchar(50) = null,
@searchTheme varchar(50) = null,
@searchSkill varchar(50) = null,
@search varchar(100) = null,
@sortBy varchar(max) = null,
@pageNumber INT = 1,
@pageSize int = 9,
@TotalCount decimal output,
@MissionCount int output,
@userId bigint 
AS
BEGIN
--Set @TotalCount = select count(mission_id) from mission

    
	SELECT 	(SELECT STRING_AGG(skill_id, ',')  FROM mission_skill WHERE mission_skill.mission_id = mission.mission_id) AS skill,
		--(select (sum(mission_rating.rating) / count(user_id) ) FROM mission_rating WHERE mission_rating.mission_id=mission.mission_id) as Rating,
		Rating.AvgRating as Rating,
		(select mission_application.approval_status from mission_application where (mission_application.user_id = @userId and mission_application.mission_id = mission.mission_id)) as approvalStatus,
		mission.country_id as countryId,
		mission.mission_id as missionID,
        city.name as cityName, 
        mission_theme.title as themeName,	
        mission.title as missionTitle, 
        mission.short_description as missionShortDesc, 
        mission.organization_name as organizationName, 
        mission.start_date as startDate, 
        mission.end_date as endDate, 
        mission.total_seats as availableSeats, 
        goal_mission.goal_objective_text as goalObjective,
		goal_mission.goal_value as goalValue,
        mission_media.media_name as missionImage,
        mission_media.media_path as mediaPath,
        mission_media.media_type as mediaType,
		mission.deadline as deadline,
		favourite_mission.favourite_mission_id as favMissionId,
		    ROW_NUMBER() OVER (ORDER BY mission.mission_id) AS RowNum

	   into #temp 
    FROM 
        mission 
        left JOIN goal_mission ON mission.mission_id = goal_mission.mission_id 
		left join mission_media ON mission_media.mission_media_id = (
		select top 1 mission_media_id from mission_media where mission_media.mission_id = mission.mission_id
		)
        left JOIN mission_theme ON mission.theme_id = mission_theme.mission_theme_id 
        left JOIN city ON mission.city_id = city.city_id 
		--left JOIN [mission_rating] ON [mission_rating].mission_id = mission.mission_id
		left JOIN favourite_mission ON favourite_mission.mission_id = mission.mission_id and favourite_mission.user_id = @userId
		LEFT JOIN (
SELECT mission_id,
CAST(AVG(rating) AS DECIMAL(5,2)) AS AvgRating
FROM mission_rating
GROUP BY mission_id
) AS Rating ON mission.mission_id = Rating.mission_id

	where
	(@search is null or (
	mission.title like '%' + @search + '%' OR
	mission.short_description like '%' + @search + '%'))  and
	
(
 @searchCountry IS NULL OR
 mission.country_id in (select value from STRING_SPLIT(@searchCountry, ','))
)
and
(
@searchCity is null or
mission.city_id in (select value from STRING_SPLIT(@searchCity, ','))
)
and
(
@searchTheme is null or
mission_theme.mission_theme_id in (select value from STRING_SPLIT(@searchTheme, ','))
)
and
(
@searchSkill is null or
mission.mission_id in (SELECT mission_id  FROM mission_skill WHERE skill_id in  (select value from STRING_SPLIT(@searchSkill, ',')))
)
and 
mission.status = 1



	
SET @TotalCount = ceiling (  (SELECT COUNT(*) FROM #temp)*1.0) /  (@pageSize*1.0)
set @MissionCount = (SELECT COUNT(*) FROM #temp)

select * from #temp 
WHERE RowNum BETWEEN ((@pageNumber - 1) * @pageSize + 1) AND (@pageNumber * @pageSize)
ORDER BY
        CASE WHEN @sortBy = 'Newest' THEN startDate END DESC,
        CASE WHEN @sortBy = 'Oldest' THEN startDate END ASC,
        CASE WHEN @sortBy = 'Highest Seats' THEN availableSeats END DESC,
        CASE WHEN @sortBy = 'Lowest Seats' THEN availableSeats END ASC

  
    SET @TotalCount = CEILING((SELECT COUNT(*) FROM #temp) * 1.0 / (@pageSize * 1.0))
END