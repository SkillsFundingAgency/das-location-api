CREATE TABLE [dbo].[PostcodeOutcode]
(
	[OutCode] varchar(4) not null CONSTRAINT PK_PostcodeOutcode PRIMARY KEY,
	[AreaName] varchar(200) not null, 
	[PostalTown] varchar(200) not null,
)
