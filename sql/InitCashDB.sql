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

INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (1, 'Dollar', 135)
INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (2, 'Dollar', 443)
INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (5, 'Dollar', 51)
INSERT INTO [dbo].[Notes] ([Nominal], [Currency], [Count]) VALUES (10, 'Dollar', 79)
GO

INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (1, 'Dollar', 532)
INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (5, 'Dollar', 735)
INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (10, 'Dollar', 344)
INSERT INTO [dbo].[Coins] ([Nominal], [Currency], [Count]) VALUES (25, 'Dollar', 975)
GO
