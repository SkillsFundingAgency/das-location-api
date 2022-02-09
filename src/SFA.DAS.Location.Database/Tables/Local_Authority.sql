CREATE TABLE [dbo].[Local_Authority]
([LocalAuthority] VARCHAR(256) NOT NULL, [UnitaryCouncil] VARCHAR(256) NULL)
GO

CREATE INDEX [IXU_Local_Authority] ON [dbo].[Local_Authority] ([LocalAuthority]) INCLUDE ( [UnitaryCouncil])
GO

CREATE INDEX [IX_Local_Authority] ON [dbo].[Local_Authority] ( [UnitaryCouncil]) INCLUDE ( [LocalAuthority] )
GO
