CREATE TABLE[dbo].[ONS_data]
(
[Place_Name_Identifier]	varchar(400),
[Place_Name_Code]	varchar(400),
[Place_Name]	varchar(400),
[Split_Place_Name_Indicator]	varchar(400),
[Place_Name_Description]	varchar(400),
[Country_Name]	varchar(400),
[County_Code]	varchar(400),
[County_Name]	varchar(400),
[Local_Authority_District_Code]	varchar(400),
[Local_Authority_District_Name]	varchar(400),
[Local_Authority_District_Description]	varchar(400),
[Region_Code]	varchar(400),
[Region_Name]	varchar(400),
[Latitude]	varchar(400),
[Longitude]	varchar(400)
)
GO

CREATE INDEX [IX_ONS_data_Place_Name] ON [dbo].[ONS_data] ([Place_Name]) INCLUDE ([County_Name],[Local_Authority_District_Name],[Region_Name],[Latitude],[Longitude])
GO

CREATE INDEX [IX_ONS_data_Local_Authority_District_Name] ON [dbo].[ONS_data] ([Local_Authority_District_Name]) INCLUDE ([County_Name],[Place_Name],[Region_Name],[Latitude],[Longitude])
GO