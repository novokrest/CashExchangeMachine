USE [master]
CREATE DATABASE [CashDB]
GO

USE [CashDB]
GO

CREATE TABLE [dbo].[Notes]
(
	[Nominal] INT NOT NULL,
	[Currency] VARCHAR(128) NOT NULL,
	[Count] INT NOT NULL,
	PRIMARY KEY (Nominal, Currency)
)
GO

CREATE TABLE [dbo].[Coins]
(
	[Nominal] INT NOT NULL,
	[Currency] VARCHAR(128) NOT NULL,
	[Count] INT NOT NULL,
	PRIMARY KEY (Nominal, Currency)
)
GO

INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (1, 'Dollar', 0)
INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (2, 'Dollar', 0)
INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (5, 'Dollar', 0)
INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (10, 'Dollar', 0)
GO

INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (1, 'Dollar', 0)
INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (5, 'Dollar', 0)
INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (10, 'Dollar', 0)
INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (25, 'Dollar', 0)
GO

