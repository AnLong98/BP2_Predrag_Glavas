--====================================
--  Create database trigger template 
--====================================
CREATE TRIGGER AddInvoiceOnRegistering
ON [dbo].[Registrations]
FOR INSERT
ASdeclare @regRunner varchar(100);
declare @regRace int;
select @regRunner=i.Runner_email from inserted i;
select @regRace=i.Race_raceID from inserted i;

insert into Invoices
(invoiceCode, Registration_Runner_email, Registration_Race_raceID)
values(NEWID(), @regRunner, @regRace);
GO


