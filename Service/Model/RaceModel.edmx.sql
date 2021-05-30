
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/28/2021 22:37:45
-- Generated from EDMX file: C:\Users\Predrag\source\repos\Race\Service\Model\RaceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RaceCenter];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Chronometer_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Chronometer] DROP CONSTRAINT [FK_Chronometer_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_EventOrganizer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_EventOrganizer];
GO
IF OBJECT_ID(N'[dbo].[FK_MeasuresTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Races] DROP CONSTRAINT [FK_MeasuresTime];
GO
IF OBJECT_ID(N'[dbo].[FK_Organizer_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Organizer] DROP CONSTRAINT [FK_Organizer_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_PaidFor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_PaidFor];
GO
IF OBJECT_ID(N'[dbo].[FK_RaceEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Races] DROP CONSTRAINT [FK_RaceEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistrateredOn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Registrations] DROP CONSTRAINT [FK_RegistrateredOn];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistrationInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoices] DROP CONSTRAINT [FK_RegistrationInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistrationRace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Registrations] DROP CONSTRAINT [FK_RegistrationRace];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultRegistration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_ResultRegistration];
GO
IF OBJECT_ID(N'[dbo].[FK_Runner_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Runner] DROP CONSTRAINT [FK_Runner_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Supervising]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Organizer] DROP CONSTRAINT [FK_Supervising];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Invoices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoices];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO
IF OBJECT_ID(N'[dbo].[Races]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Races];
GO
IF OBJECT_ID(N'[dbo].[Registrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Registrations];
GO
IF OBJECT_ID(N'[dbo].[Results]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Results];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Users_Chronometer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Chronometer];
GO
IF OBJECT_ID(N'[dbo].[Users_Organizer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Organizer];
GO
IF OBJECT_ID(N'[dbo].[Users_Runner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Runner];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Results'
CREATE TABLE [dbo].[Results] (
    [tagID] int IDENTITY(1,1) NOT NULL,
    [hour] int  NOT NULL,
    [minute] int  NOT NULL,
    [second] int  NOT NULL,
    [Registration_Runner_email] nvarchar(100)  NOT NULL,
    [Registration_Race_raceID] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [paymentID] int IDENTITY(1,1) NOT NULL,
    [moneyPaid] float  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [Invoice_invoiceCode] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [invoiceCode] uniqueidentifier  NOT NULL,
    [Registration_Runner_email] nvarchar(100)  NOT NULL,
    [Registration_Race_raceID] int  NOT NULL
);
GO

-- Creating table 'Registrations'
CREATE TABLE [dbo].[Registrations] (
    [startNumber] int  NOT NULL,
    [Runner_email] nvarchar(100)  NOT NULL,
    [Race_raceID] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [eventID] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [website] nvarchar(max)  NULL,
    [bankAccount] nvarchar(max)  NOT NULL,
    [startDate] datetime  NOT NULL,
    [endDate] datetime  NOT NULL,
    [Organizer_email] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Races'
CREATE TABLE [dbo].[Races] (
    [raceID] int IDENTITY(1,1) NOT NULL,
    [distance] float  NOT NULL,
    [date] datetime  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [minStartNumber] int  NOT NULL,
    [maxStartNumber] int  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [startFarePrice] float  NOT NULL,
    [Chronometer_email] nvarchar(100)  NULL,
    [Event_eventID] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [email] nvarchar(100)  NOT NULL,
    [firstName] nvarchar(max)  NOT NULL,
    [lastName] nvarchar(max)  NOT NULL,
    [address] nvarchar(max)  NULL,
    [password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users_Organizer'
CREATE TABLE [dbo].[Users_Organizer] (
    [isAdmin] bit  NOT NULL,
    [phoneNumber] nvarchar(max)  NULL,
    [supervisorEmail] nvarchar(100)  NULL,
    [email] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Users_Chronometer'
CREATE TABLE [dbo].[Users_Chronometer] (
    [companyName] nvarchar(max)  NOT NULL,
    [email] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Users_Runner'
CREATE TABLE [dbo].[Users_Runner] (
    [club] nvarchar(max)  NULL,
    [email] nvarchar(100)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [tagID] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [PK_Results]
    PRIMARY KEY CLUSTERED ([tagID] ASC);
GO

-- Creating primary key on [paymentID] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([paymentID] ASC);
GO

-- Creating primary key on [invoiceCode] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([invoiceCode] ASC);
GO

-- Creating primary key on [Runner_email], [Race_raceID] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [PK_Registrations]
    PRIMARY KEY CLUSTERED ([Runner_email], [Race_raceID] ASC);
GO

-- Creating primary key on [eventID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([eventID] ASC);
GO

-- Creating primary key on [raceID] in table 'Races'
ALTER TABLE [dbo].[Races]
ADD CONSTRAINT [PK_Races]
    PRIMARY KEY CLUSTERED ([raceID] ASC);
GO

-- Creating primary key on [email] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([email] ASC);
GO

-- Creating primary key on [email] in table 'Users_Organizer'
ALTER TABLE [dbo].[Users_Organizer]
ADD CONSTRAINT [PK_Users_Organizer]
    PRIMARY KEY CLUSTERED ([email] ASC);
GO

-- Creating primary key on [email] in table 'Users_Chronometer'
ALTER TABLE [dbo].[Users_Chronometer]
ADD CONSTRAINT [PK_Users_Chronometer]
    PRIMARY KEY CLUSTERED ([email] ASC);
GO

-- Creating primary key on [email] in table 'Users_Runner'
ALTER TABLE [dbo].[Users_Runner]
ADD CONSTRAINT [PK_Users_Runner]
    PRIMARY KEY CLUSTERED ([email] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [supervisorEmail] in table 'Users_Organizer'
ALTER TABLE [dbo].[Users_Organizer]
ADD CONSTRAINT [FK_Supervising]
    FOREIGN KEY ([supervisorEmail])
    REFERENCES [dbo].[Users_Organizer]
        ([email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Supervising'
CREATE INDEX [IX_FK_Supervising]
ON [dbo].[Users_Organizer]
    ([supervisorEmail]);
GO

-- Creating foreign key on [Chronometer_email] in table 'Races'
ALTER TABLE [dbo].[Races]
ADD CONSTRAINT [FK_MeasuresTime]
    FOREIGN KEY ([Chronometer_email])
    REFERENCES [dbo].[Users_Chronometer]
        ([email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MeasuresTime'
CREATE INDEX [IX_FK_MeasuresTime]
ON [dbo].[Races]
    ([Chronometer_email]);
GO

-- Creating foreign key on [Event_eventID] in table 'Races'
ALTER TABLE [dbo].[Races]
ADD CONSTRAINT [FK_RaceEvent]
    FOREIGN KEY ([Event_eventID])
    REFERENCES [dbo].[Events]
        ([eventID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RaceEvent'
CREATE INDEX [IX_FK_RaceEvent]
ON [dbo].[Races]
    ([Event_eventID]);
GO

-- Creating foreign key on [Runner_email] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [FK_RegistrateredOn]
    FOREIGN KEY ([Runner_email])
    REFERENCES [dbo].[Users_Runner]
        ([email])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Organizer_email] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_EventOrganizer]
    FOREIGN KEY ([Organizer_email])
    REFERENCES [dbo].[Users_Organizer]
        ([email])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventOrganizer'
CREATE INDEX [IX_FK_EventOrganizer]
ON [dbo].[Events]
    ([Organizer_email]);
GO

-- Creating foreign key on [Invoice_invoiceCode] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_PaidFor]
    FOREIGN KEY ([Invoice_invoiceCode])
    REFERENCES [dbo].[Invoices]
        ([invoiceCode])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaidFor'
CREATE INDEX [IX_FK_PaidFor]
ON [dbo].[Payments]
    ([Invoice_invoiceCode]);
GO

-- Creating foreign key on [Race_raceID] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [FK_RegistrationRace]
    FOREIGN KEY ([Race_raceID])
    REFERENCES [dbo].[Races]
        ([raceID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistrationRace'
CREATE INDEX [IX_FK_RegistrationRace]
ON [dbo].[Registrations]
    ([Race_raceID]);
GO

-- Creating foreign key on [Registration_Runner_email], [Registration_Race_raceID] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [FK_RegistrationInvoice]
    FOREIGN KEY ([Registration_Runner_email], [Registration_Race_raceID])
    REFERENCES [dbo].[Registrations]
        ([Runner_email], [Race_raceID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistrationInvoice'
CREATE INDEX [IX_FK_RegistrationInvoice]
ON [dbo].[Invoices]
    ([Registration_Runner_email], [Registration_Race_raceID]);
GO

-- Creating foreign key on [Registration_Runner_email], [Registration_Race_raceID] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_ResultRegistration]
    FOREIGN KEY ([Registration_Runner_email], [Registration_Race_raceID])
    REFERENCES [dbo].[Registrations]
        ([Runner_email], [Race_raceID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultRegistration'
CREATE INDEX [IX_FK_ResultRegistration]
ON [dbo].[Results]
    ([Registration_Runner_email], [Registration_Race_raceID]);
GO

-- Creating foreign key on [email] in table 'Users_Organizer'
ALTER TABLE [dbo].[Users_Organizer]
ADD CONSTRAINT [FK_Organizer_inherits_User]
    FOREIGN KEY ([email])
    REFERENCES [dbo].[Users]
        ([email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [email] in table 'Users_Chronometer'
ALTER TABLE [dbo].[Users_Chronometer]
ADD CONSTRAINT [FK_Chronometer_inherits_User]
    FOREIGN KEY ([email])
    REFERENCES [dbo].[Users]
        ([email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [email] in table 'Users_Runner'
ALTER TABLE [dbo].[Users_Runner]
ADD CONSTRAINT [FK_Runner_inherits_User]
    FOREIGN KEY ([email])
    REFERENCES [dbo].[Users]
        ([email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------