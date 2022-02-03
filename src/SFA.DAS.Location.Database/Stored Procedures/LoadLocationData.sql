CREATE PROCEDURE [dbo].[LoadLocationData]
	@FilePath varchar(1000)
AS
	EXEC [dbo].[ONSFileload] @FilePath;
	
	EXEC [dbo].[SetupLocations];

RETURN 0
