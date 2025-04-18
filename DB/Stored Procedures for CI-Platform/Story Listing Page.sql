USE [CI_Platform]
GO
/****** Object:  StoredProcedure [dbo].[StoryListing]    Script Date: 23-03-2023 12:59:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[StoryListing]
AS
BEGIN
	SELECT
		story.story_id as storyID,
		story.mission_id as storymissionID,
		story.user_id as storyuserID,
		story.description as storyDescription,
		story.title as storyTitle,
		story.status as storyApprovalStatus,
		story_media.path as storyImagePath,
		story_media.type as storyImageType,
		[user].first_name as firstName,
		[user].last_name as lastName,
		[user].avatar as Avatar,
		mission_theme.title as missionTheme

	FROM
		story

	INNER JOIN story_media
		ON story_media.story_id = story.story_id
	INNER JOIN [user]
		ON [user].user_id = story.user_id
	INNER JOIN mission
		ON mission.mission_id = story.mission_id
	INNER JOIN mission_theme 
		ON mission_theme.mission_theme_id = mission.theme_id

	Where
		story.status = 'Approved' 

	
END