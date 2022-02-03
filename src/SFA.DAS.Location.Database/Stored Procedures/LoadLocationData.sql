CREATE PROCEDURE [dbo].[LoadLocationData]
	@FilePath varchar(1000), 
    @Filename NVARCHAR(500)
AS
	EXEC [dbo].[ONSFileload] @FilePath, @Filename;
	
	EXEC [dbo].[SetupLocations];

RETURN 0
