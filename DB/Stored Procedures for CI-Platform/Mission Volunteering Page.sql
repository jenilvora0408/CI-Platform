USE [CI_Platform]
GO
/****** Object:  StoredProcedure [dbo].[GetMissionVolData]    Script Date: 23-03-2023 10:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetMissionVolData]
@missionId INT =NULL,
@userId INT =NULL  
AS
BEGIN

	SELECT 	(SELECT STRING_AGG(skill.skill_name, ',')  FROM skill WHERE skill.skill_id = mission_skill.skill_id) AS skill,
	(select (sum(mission_rating.rating) / count(user_id) ) FROM mission_rating WHERE mission_rating.mission_id=mission.mission_id) as Rating,
	(select count (mission_rating.user_id) FROM mission_rating WHERE mission_rating.mission_id=mission.mission_id) as ratingvolcount,
		@userId as Navbar_1userId,
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
        mission.description as introduction,
		mission.availability as days,
		mission.organization_detail as organizationDetail,
		mission.deadline as deadline,
		favourite_mission.favourite_mission_id as favMissionId,
		--mission_application.approval_status as approvalStatus,
	(case when (
	select count(*) from mission_application where mission_application.mission_id = mission.mission_id and mission_application.user_id = @userId and mission_application.approval_status = 'Approved'
	) > 0 then 1   
	 when (
	select count(*) from mission_application where mission_application.mission_id = mission.mission_id and mission_application.user_id = @userId and mission_application.approval_status = 'Pending'
	) > 0 then 0 end
	) as approvalStatus
				
	   
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
		left join mission_application ON mission_application.mission_id = mission.mission_id
		WHERE mission.mission_id=@missionId;


END