/* 
Apply the ONS staging data to the Locations table, using the Local Authority & Unitary Authority data
*/


PRINT '-------- Start Setup Location data ----------';

PRINT '-------- Remove previous data from Location_Import ----------';
DELETE FROM [dbo].[Location_Import] WHERE Id IS NOT NULL;

PRINT '-------- Add fresh Location data to Location_Import ----------';
INSERT INTO [dbo].[Location_Import] ([Id], [LocationName], [CountyName], [LocalAuthorityName], [Lat], [Long], [LocalAuthorityDistrict], [Region], [LocationType])
SELECT 
 ROW_NUMBER() OVER (ORDER BY [Place_Name],[County_Name]) Id
,[Place_Name] [LocationName]
,[County_Name] [CountyName]
,ISNULL(la1.[UnitaryCouncil],ab1.[LocalAuthorityDistrict]) [LocalAuthorityName]
,CAST([Latitude] AS FLOAT) Lat
,CAST([Longitude] AS FLOAT) Long
,ab1.[LocalAuthorityDistrict]
,ab1.[Region_Name] [Region]
,[LocationType]
FROM (
SELECT ROW_NUMBER() OVER (PARTITION BY 
                          CASE WHEN [place20nm] LIKE '% BUA' 
                               THEN REPLACE([place20nm],' BUA','') 
                               WHEN [place20nm] LIKE '% BUASD' 
                               THEN REPLACE([place20nm],' BUASD','') 
                               ELSE [place20nm] END,[lad20nm] 
                          ORDER BY CASE [descnm] WHEN 'LOC' THEN 1 WHEN 'PAR' THEN 2 WHEN 'BUA' THEN 3 WHEN 'BUASD' THEN 4 ELSE 5 END, [place20cd]) seq
        ,[placeid] [Place_Name_Identifier]
        ,CASE WHEN [place20nm] LIKE '% BUA' 
              THEN REPLACE([place20nm],' BUA','') 
              WHEN [place20nm] LIKE '% BUASD' 
              THEN REPLACE([place20nm],' BUASD','') 
              ELSE [place20nm]  END [Place_Name]
        ,ISNULL([cty20nm],[ctyltnm]) [County_Name]
        ,[lad20nm] [LocalAuthorityDistrict]
        ,[rgn20nm] [Region_Name]
        ,[Lat] [Latitude]
        ,[Long] [Longitude]
	    ,[descnm] [LocationType]
    FROM [dbo].[ONS_staging] 
    WHERE [ctry20nm] = 'England' 
    AND [descnm] IN ( 'LOC' ,'PAR', 'WD', 'BUA', 'BUASD' )
    AND [lad20nm] IS NOT NULL
    ) ab1 
JOIN [dbo].[Local_Authority] la1 ON la1.[LocalAuthority] = ab1.[LocalAuthorityDistrict]
WHERE seq = 1  -- this filters out parish (PAR), built-up areas (BUA,BUASD) and ward (WD) that share the same name as the place (LOC) they represent.
 
BEGIN
    DECLARE @sqlcount int, @diffcount int;
    SELECT @sqlcount = COUNT(*) FROM [dbo].[Location_Import];
    IF @sqlcount = 0 
    BEGIN
    -- need to fail
        PRINT '-------- No records in to Location_Import table ----------';
        THROW 50000, 'No location data was imported', 1;
    END
    ELSE
    BEGIN
        PRINT '-------- Loaded '+CONVERT(varchar,@sqlcount)+' records in to Location_Import table ----------';
        SELECT @diffcount = COUNT(*) FROM 
           (SELECT [LocationName],[CountyName],[LocalAuthorityName],[Lat],[Long],[LocalAuthorityDistrict],[Region],[LocationType] FROM [dbo].[Location_Import]
            EXCEPT 
            SELECT [LocationName],[CountyName],[LocalAuthorityName],[Lat],[Long],[LocalAuthorityDistrict],[Region],[LocationType] FROM [dbo].[Location]
           ) imp
        PRINT '-------- Found '+CONVERT(varchar,@diffcount)+' different records between Location_Import and Location tables ----------';
        IF @diffcount = 0 
            PRINT '-------- No changes found in Location_Import table ----------';
        ELSE
        BEGIN
            DELETE FROM [dbo].[Location] WHERE Id IS NOT NULL;
            INSERT INTO [dbo].[Location] SELECT * FROM [dbo].[Location_Import]
            PRINT '-------- Updated Location table with contents from Location_Import table ----------';
        END
    END
END;


PRINT '-------- Ended Setup Location data ----------';
 GO
