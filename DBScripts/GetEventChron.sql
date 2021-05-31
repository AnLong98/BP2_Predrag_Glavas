-- ================================================
-- Template generated from Template Explorer using:
-- Create Multi-Statement Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetEventChronometers]
(
	@P_Event_ID int
) RETURNS  @returnTable TABLE (email nvarchar(max))
AS
BEGIN
	
	insert into @returnTable
	SELECT DISTINCT email
    FROM Users_Chronometer AS Chrono
    JOIN Races AS Race ON Race.Chronometer_email = Chrono.email
    JOIN Events AS Ev ON Ev.eventID = Race.Event_eventID
    WHERE Ev.eventID = @P_Event_ID
	RETURN;
END;
GO