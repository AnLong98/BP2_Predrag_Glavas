
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ConsolidateStartersList(
	@Race_ID int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @Curr_Email nvarchar(max)
DECLARE @Race_curr_bib int = 0

DECLARE db_cursor CURSOR FOR

SELECT email
FROM dbo.Users_Runner as Runn
JOIN Registrations AS Reg
ON Runn.email = Reg.Runner_email
WHERE Reg.Race_raceID = @Race_ID

SELECT @Race_curr_bib = minStartNumber FROM Races
WHERE raceID = @Race_ID


-- Open the Cursor
OPEN db_cursor

-- 3 - Fetch the next record from the cursor
FETCH NEXT FROM db_cursor INTO @Curr_Email
-- Set the status for the cursor
BEGIN TRANSACTION;
WHILE @@FETCH_STATUS = 0  
 
	BEGIN  
		UPDATE Registrations
		SET startNumber = @Race_curr_bib
		WHERE Runner_email = @Curr_Email AND Race_raceID = @Race_ID;

		SELECT @Race_curr_bib = @Race_curr_bib + 1
 		FETCH NEXT FROM db_cursor INTO @Curr_Email
	END 
COMMIT;
-- 6 - Close the cursor
CLOSE db_cursor  

-- 7 - Deallocate the cursor
DEALLOCATE db_cursor 
END
GO
