CREATE PROCEDURE GetMissionData
AS
BEGIN
    SELECT 
        country.name, 
        mission_theme.title, 
        mission.title, 
        mission.short_description, 
        mission.organization_name, 
        mission.start_date, 
        mission.end_date, 
        mission.availability, 
        goal_mission.goal_objective_text, 
        mission_media.media_name
    FROM 
        mission 
        INNER JOIN goal_mission ON mission.mission_id = goal_mission.mission_id 
        INNER JOIN mission_theme ON mission.mission_id = mission_theme.mission_theme_id 
        INNER JOIN country ON mission.mission_id = country.country_id 
        INNER JOIN mission_media ON mission.mission_id = mission_media.mission_id
END


execute GetMissionData