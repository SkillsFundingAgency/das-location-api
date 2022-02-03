CREATE PROCEDURE [dbo].[ONSFileload]
	@FilePath varchar(1000), 
    @Filename NVARCHAR(500)
AS

-- Loads ONS Places file as held in csv file

-- uses parameters
-- @FilePath is the Directory Path for BLOB Storage, e.g. "https://bigdevtest.blob.core.windows.net/datafiles"
-- @Filename is the file name, currently using "IPN_GB_2021.csv"
/*    if working locally you can load directly from the Git directory using 
      @FilePath = 'C:\<path to git>\GiT\das-location-api\src\SFA.DAS.Location.Database\data'
      @Filename = 'IPN_GB_2021.csv'

     if you want to use AZURE BLOB Storage (to test) you will need a storage account and set the values in the 
     @FilePath = this should be the path to the storage account starting with "http", but for now can just be "http"
        The path is not currently used for the load, other than to tell the script to use BLOB storage container
        Ideally, the contents of the container could be determined dynamically (in T-SQL) - but so far this has eluded me ¯\_(ツ)_/¯
        In additon to setting @FilePath the database requires an external data source to be setup:

		1. Create a database master key if one does not already exist in your local database, using your own password. 
           This key is used to encrypt the credential secret in next step.
		CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'MaryHadALittleLAmb';
		
		2. Create a database scoped credential using your (test) Azure storage account shared access secret.
		CREATE DATABASE SCOPED CREDENTIAL AzureBlobCredential WITH IDENTITY = 'SHARED ACCESS SIGNATURE', SECRET = '<shared access secret>';

	    3. Create an external data source with CREDENTIAL option to the BLOB Storage URL as <URL>
		CREATE EXTERNAL DATA SOURCE BlobStorage WITH (LOCATION = '<URL>',CREDENTIAL = AzureBlobCredential,TYPE = BLOB_STORAGE);

    For production only BLOB storage can be used, and the Database will need an External Data Source 'BlobStorage' 
    and the file will need to be copied from das-location-api\src\SFA.DAS.Location.Database\data
    or the file that has been downloaded can be found using
    https://geoportal.statistics.gov.uk/datasets/ons::index-of-place-names-in-great-britain-november-2021/about
    Which will provides a zipped file containing the csv file and documentation.
    https://www.arcgis.com/sharing/rest/content/items/e786420d47c84dbfbbfbc0f6670d5474/data
    
    Note, that later versions of the ONS data will need to be found in a different location
    and wouldhave a different file name.

*/


DECLARE @FileLocation VARCHAR(1000) = @FilePath;

DECLARE @LoadBLOB BIT = 0;  -- assume local - set to 1 if @FilePath starts with http

BEGIN
    DECLARE @sql NVARCHAR(4000),
            @sqlcount INT;

    -- START
    SET @FileLocation = @FilePath;

    -- find the files in BLOB Storage (or Local Dev)

    IF SUBSTRING(@FileLocation,1,4) = 'http'
    BEGIN
        SET @LoadBLOB = 1;
        SET @FileLocation = '/';
        PRINT 'Loading from BLOB Storage '+@FileLocation;
    END
    ELSE
    BEGIN
        SET @FileLocation = @FileLocation + '\';
        PRINT 'Loading from local File Storage: '+@FileLocation;
    END
    
    
    PRINT '-------- Load ONS data File --------';  
    DELETE FROM [dbo].[ons_staging];
  
    IF @LoadBLOB = 1
        SET @sql = 'BULK INSERT [dbo].[ons_staging] FROM '''+@Filename+'''
                    WITH ( DATA_SOURCE = ''BlobStorage'', FORMAT = ''CSV'', FIRSTROW=2, CODEPAGE=''ACP'' )';            
    ELSE
        SET @sql = 'BULK INSERT [dbo].[ons_staging] FROM '''+@FileLocation+@Filename+'''
                    WITH ( FORMAT = ''CSV'', FIRSTROW=2, CODEPAGE=''ACP'' )';            

    PRINT '-------- Load ONS data File using '+@sql+ ' --------';  


    EXECUTE (@sql);
    
    SELECT @sqlcount = COUNT(*) FROM [dbo].[ons_staging];

    PRINT '-------- Loaded '+CONVERT(varchar,@sqlcount)+' ONS data records ----------';

    PRINT '-------- Patch Local Authority in ONS staging ----------';

    -- patch 'Bournemouth, Christchurch and Poole' 
    UPDATE [ons_staging] 
    SET  lad20nm = 'Bournemouth, Christchurch and Poole' 
    WHERE lad20nm ='Bournemouth, Christchurch an';

    /*
    The North Northamptonshire unitary will cover Corby, East Northants, Kettering and Wellingborough 
    and the West Northamptonshire unitary will cover Daventry District, Northampton and South Northamptonshire. 
    The existing district and borough councils and Northamptonshire County Council will all be abolished.
    */
    UPDATE[ons_staging]
    SET lad20nm  =
    CASE lad20nm 
    WHEN 'Daventry' THEN 'West Northamptonshire'
    WHEN 'Northampton' THEN 'West Northamptonshire'
    WHEN 'South Northamptonshire' THEN 'West Northamptonshire'
    WHEN 'Corby' THEN 'North Northamptonshire'
    WHEN 'Kettering' THEN 'North Northamptonshire'
    WHEN 'Wellingborough' THEN 'North Northamptonshire'
    WHEN 'East Northamptonshire' THEN 'North Northamptonshire'
    ELSE lad20nm END 
    WHERE lad20nm IN ('Daventry' , 'Northampton', 'South Northamptonshire', 'Corby', 'Kettering', 'Wellingborough', 'East Northamptonshire' );

    PRINT '-------- Local Authority updated in ONS staging ----------';


RETURN 0;
END;


