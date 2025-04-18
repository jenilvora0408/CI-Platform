USE [CI_Platform]
GO
/****** Object:  StoredProcedure [dbo].[RelatedMissionData]    Script Date: 23-03-2023 15:00:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RelatedMissionData]
@missionID int,
@themeTitle nvarchar(100) =NULL
AS
BEGIN

	SELECT top 3 
		(select (sum(mission_rating.rating) / count(user_id) ) FROM mission_rating WHERE mission_rating.mission_id=mission.mission_id) as Rating,
		mission.mission_id as missionId,
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
		favourite_mission.favourite_mission_id as favMissionId
				
	   
    FROM 
        mission 
        left JOIN goal_mission ON mission.mission_id = goal_mission.mission_id
		 left join mission_media ON mission_media.mission_media_id = (
		select top 1 mission_media_id from mission_media where mission_media.mission_id = mission.mission_id
		)
		--left join mission_media ON mission_media.mission_media_id = (
		--select top 1 mission_media_id from mission_media where mission_media.mission_id = mission.mission_id
		--)
        left JOIN mission_theme ON mission.theme_id = mission_theme.mission_theme_id 
        left JOIN city ON mission.city_id = city.city_id 
		left join mission_skill on mission_skill.mission_id = mission.mission_id
		left join skill on skill.skill_id = mission_skill.skill_id
		left JOIN favourite_mission ON favourite_mission.mission_id = mission.mission_id
		
		where mission_theme.title = @themeTitle and mission.mission_id != @missionID

END