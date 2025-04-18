USE [CI_Platform]
GO
/****** Object:  StoredProcedure [dbo].[recentVolunteer]    Script Date: 23-03-2023 10:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[recentVolunteer]
@missionid int
as
begin

select
u.user_id as UserId,
u.first_name as FirstName,  
u.last_name as LastName,
u.avatar as Avatar

from [user] as u
inner join mission_application as m on u.user_id = m.user_id

Where
m.mission_id = @missionid
and m.approval_status = 'Approved'

end