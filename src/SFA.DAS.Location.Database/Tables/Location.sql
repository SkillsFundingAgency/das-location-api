CREATE TABLE [dbo].[Location]
(
    [Id] INT PRIMARY KEY,
	[LocationName] VARCHAR(256) NOT NULL,
	[CountyName] VARCHAR(256) NULL,
	[LocalAuthorityName] VARCHAR(256) NULL,
	[Lat] FLOAT NOT NULL,
	[Long] FLOAT NOT NULL,
	[LocalAuthorityDistrict] VARCHAR(256) NULL,
	[Region]  VARCHAR(256) NULL,
	[LocationType] VARCHAR(20) NULL
)
GO

CREATE NONCLUSTERED INDEX [IDX_Location_Name] ON [dbo].[Location] (LocationName) 
INCLUDE (Id, CountyName, LocalAuthorityName, Lat, Long) WITH (ONLINE = ON) 
GO 