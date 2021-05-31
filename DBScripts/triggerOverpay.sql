--====================================
--  Create database trigger template 
--====================================
CREATE TRIGGER PreventOverpaying
ON [dbo].[Payments]
AFTER INSERT
AS
DECLARE @totalToPay decimal, @paid int, @invCode UNIQUEIDENTIFIER;
SELECT @paid=i.moneyPaid from inserted i;
SELECT @invCode=i.Invoice_invoiceCode from inserted i;
SELECT @totalToPay = startFarePrice
           FROM Races AS Ra
		   JOIN Registrations Reg
		   On Ra.raceID = Reg.Race_raceID
		   JOIN Invoices Inv
		   On Reg.Race_raceID = Inv.Registration_Race_raceID AND Reg.Runner_email = Inv.Registration_Runner_email
		   JOIN Payments AS Pay
		   On Pay.Invoice_invoiceCode = Inv.invoiceCode
           WHERE Pay.Invoice_invoiceCode =  @invCode;
          

if(@totalToPay < @paid)
BEGIN
	RAISERROR ('Payment cannot be inserted as it overpays invoice.', 16, 1);
	ROLLBACK TRANSACTION;

END;
GO


