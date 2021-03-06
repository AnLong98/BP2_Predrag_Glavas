USE [RaceCenter]
GO
/****** Object:  StoredProcedure [dbo].[GetRunnerExpenses]    Script Date: 30-May-21 8:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRunnerExpenses](
	@Runner_email nvarchar(max),
	@Total_Expense decimal out
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE db_cursor CURSOR FOR

SELECT Race_raceID
FROM dbo.Registrations
WHERE Runner_email = @Runner_email

DECLARE @Race_ID int 
DECLARE @Sum_Expenses decimal = 0
SELECT @Total_Expense = 0


-- Open the Cursor
OPEN db_cursor

-- 3 - Fetch the next record from the cursor
FETCH NEXT FROM db_cursor INTO @Race_ID 

-- Set the status for the cursor
WHILE @@FETCH_STATUS = 0  
 
	BEGIN  
		Select @Sum_Expenses =  totalMoney FROM dbo.[GetTotalRegistrationPayments](@Runner_email, @Race_ID)
		IF @Sum_Expenses IS NOT NULL
			BEGIN
				SELECT @Total_Expense = @Sum_Expenses + @Total_Expense
			END
 		FETCH NEXT FROM db_cursor INTO @Race_ID 
	END 

-- 6 - Close the cursor
CLOSE db_cursor  

-- 7 - Deallocate the cursor
DEALLOCATE db_cursor 
END
