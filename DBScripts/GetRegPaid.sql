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
CREATE FUNCTION [dbo].[GetTotalRegistrationPayments]
(
	@P_Runner_Email nvarchar(max),
	@P_Race_ID int
) RETURNS  @total_paid TABLE (totalMoney decimal)
AS
BEGIN
	
	insert into @total_paid
	SELECT SUM(moneyPaid)
    FROM Payments AS P
    JOIN Invoices AS Inv ON P.Invoice_invoiceCode = Inv.invoiceCode
    JOIN Registrations AS Reg ON Reg.Race_raceID = Inv.Registration_Race_raceID AND Reg.Runner_email = Inv.Registration_Runner_email
    WHERE Reg.Race_raceID = @P_Race_ID AND Registration_Runner_email = @P_Runner_Email
	RETURN;
END;
GO