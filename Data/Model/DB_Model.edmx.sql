
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/11/2023 22:15:19
-- Generated from EDMX file: C:\Cipher\Data\Model\DB_Model.edmx
-- --------------------------------------------------
-- CREATE DATABASE CipherDB;
SET QUOTED_IDENTIFIER OFF;
GO
USE [CipherDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account_Additions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account_Additions];
GO
IF OBJECT_ID(N'[dbo].[Account_Credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account_Credentials];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Additions_Credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Additions_Credentials];
GO
IF OBJECT_ID(N'[dbo].[Entries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries];
GO
IF OBJECT_ID(N'[dbo].[Entries_Credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries_Credentials];
GO
IF OBJECT_ID(N'[dbo].[Fields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fields];
GO
IF OBJECT_ID(N'[dbo].[Fields_Credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fields_Credentials];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Account_Additions'
CREATE TABLE [dbo].[Account_Additions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Account_ID] int  NULL,
    [Name] nvarchar(max)  NULL,
    [Data] nvarchar(max)  NULL,
    [is_on_card] bit  NULL
);
GO

-- Creating table 'Account_Credentials'
CREATE TABLE [dbo].[Account_Credentials] (
    [Account_ID] int  NOT NULL,
    [Username_Key] nvarchar(max)  NULL,
    [Username_Vector] nvarchar(max)  NULL,
    [First_Name_Key] nvarchar(max)  NULL,
    [First_Name_Vector] nvarchar(max)  NULL,
    [Last_Name_Key] nvarchar(max)  NULL,
    [Last_Name_Vector] nvarchar(max)  NULL,
    [E_mail_Key] nvarchar(max)  NULL,
    [E_mail_Vector] nvarchar(max)  NULL,
    [Password_Key] nvarchar(max)  NULL,
    [Password_Vector] nvarchar(max)  NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NULL,
    [First_Name] nvarchar(max)  NULL,
    [Last_Name] nvarchar(max)  NULL,
    [E_mail] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [C2FA] bit  NULL
);
GO

-- Creating table 'Additions_Credentials'
CREATE TABLE [dbo].[Additions_Credentials] (
    [Addition_ID] int  NOT NULL,
    [Name_Key] nvarchar(max)  NULL,
    [Name_Vector] nvarchar(max)  NULL,
    [Data_Key] nvarchar(max)  NULL,
    [Data_Vector] nvarchar(max)  NULL
);
GO

-- Creating table 'Entries'
CREATE TABLE [dbo].[Entries] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Account_ID] int  NULL,
    [Name] nvarchar(max)  NULL,
    [Additional_Info] nvarchar(max)  NULL
);
GO

-- Creating table 'Entries_Credentials'
CREATE TABLE [dbo].[Entries_Credentials] (
    [Entry_ID] int  NOT NULL,
    [Name_Key] nvarchar(max)  NULL,
    [Name_Vector] nvarchar(max)  NULL,
    [Additional_Info_Key] nvarchar(max)  NULL,
    [Additional_Info_Vector] nvarchar(max)  NULL
);
GO

-- Creating table 'Fields'
CREATE TABLE [dbo].[Fields] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Entry_ID] int  NULL,
    [Name] nvarchar(max)  NULL,
    [Data] nvarchar(max)  NULL
);
GO

-- Creating table 'Fields_Credentials'
CREATE TABLE [dbo].[Fields_Credentials] (
    [Field_ID] int  NOT NULL,
    [Name_Key] nvarchar(max)  NULL,
    [Name_Vector] nvarchar(max)  NULL,
    [Data_Key] nvarchar(max)  NULL,
    [Data_Vector] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Account_Additions'
ALTER TABLE [dbo].[Account_Additions]
ADD CONSTRAINT [PK_Account_Additions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Account_ID] in table 'Account_Credentials'
ALTER TABLE [dbo].[Account_Credentials]
ADD CONSTRAINT [PK_Account_Credentials]
    PRIMARY KEY CLUSTERED ([Account_ID] ASC);
GO

-- Creating primary key on [ID] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Addition_ID] in table 'Additions_Credentials'
ALTER TABLE [dbo].[Additions_Credentials]
ADD CONSTRAINT [PK_Additions_Credentials]
    PRIMARY KEY CLUSTERED ([Addition_ID] ASC);
GO

-- Creating primary key on [ID] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [PK_Entries]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Entry_ID] in table 'Entries_Credentials'
ALTER TABLE [dbo].[Entries_Credentials]
ADD CONSTRAINT [PK_Entries_Credentials]
    PRIMARY KEY CLUSTERED ([Entry_ID] ASC);
GO

-- Creating primary key on [ID] in table 'Fields'
ALTER TABLE [dbo].[Fields]
ADD CONSTRAINT [PK_Fields]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Field_ID] in table 'Fields_Credentials'
ALTER TABLE [dbo].[Fields_Credentials]
ADD CONSTRAINT [PK_Fields_Credentials]
    PRIMARY KEY CLUSTERED ([Field_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------