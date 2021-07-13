CREATE TABLE [dbo].[ImportAudit]
(
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[TimeStarted] DATETIME NOT NULL,
	[TimeFinished] DATETIME NOT NULL,
	[RowsImported] INT NOT NULL,
	[ImportType] TINYINT NOT NULL DEFAULT(0),
    [Name] VARCHAR(500) NULL
)
GO
