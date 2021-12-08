-- Loads Ordnance Survey open names files as defined in FileList.csv

-- uses parameters
-- $(FilePath) is a SQL parameter for the Directory Path for local or BLOB
/*    if working locally you can load directly from the Git directory using 
      $(FilePath) = C:\<path to git>\GiT\das-location-api\src\SFA.DAS.Location.Database\data

     if you want to use AZURE BLOB Storage (to test) you will need a storage account and set the values in the 
     $(FilePath) = this should be the path to the storage account starting with "http", but for now can just be "http"
        The path is not currently used for the load, other than to tell the script to use BLOB storage container
        Ideally, the contents of the container could be determined dynamically (in T-SQL) - but so far this has eluded me ¯\_(ツ)_/¯
        In additon to setting $(FilePath) the database requires an external data source to be setup:

		1. Create a database master key if one does not already exist in your local database, using your own password. 
           This key is used to encrypt the credential secret in next step.
		CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'MaryHadALittleLAmb';
		
		2. Create a database scoped credential using your (test) Azure storage account shared access secret.
		CREATE DATABASE SCOPED CREDENTIAL AzureBlobCredential WITH IDENTITY = 'SHARED ACCESS SIGNATURE', SECRET = '<shared access secret>';

	    3. Create an external data source with CREDENTIAL option to the BLOB Storage URL as <URL>
		CREATE EXTERNAL DATA SOURCE BlobStorage WITH (LOCATION = '<URL>',CREDENTIAL = AzureBlobCredential,TYPE = BLOB_STORAGE);

    For production only BLOB storage can be used, and the Database will need an External Data Source 'BlobStorage' 
    and the files will need to be copied from das-location-api\src\SFA.DAS.Location.Database\data

    Ideally, the data should be refreshed perioidically, after deployment, which can be done by downloading the file using
    https://api.os.uk/downloads/v1/products/OpenNames/downloads?format=CSV&redirect=true
    Which will provide a zipped file containing all of the names files.
    The names of these files need to be placed in the Filelist.csv file in the data directory (for local use) and in the BLOB storgae for production use.

    It would be preferable to create a function app that can regularly repeat the download, unpack the zip, and load the files into a staging table,
    before replacing the current data in a transaction that will not leave the os_open_names table empty at any point.

*/


DECLARE @FileLocation VARCHAR(100) = '$(FilePath)';

DECLARE @LoadBLOB BIT = 0;  -- assume local - set to 1 if $(FilePath) starts with http

BEGIN
    DECLARE @filename NVARCHAR(500),
            @sql NVARCHAR(4000);

    DECLARE @filelist TABLE (filename VARCHAR(20));
    CREATE TABLE #filelist (filename VARCHAR(20))
    


    -- START
    SET @FileLocation = '$(FilePath)';

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
    
    PRINT '-------- Load Ordnance Survey File names --------';  
    IF @LoadBLOB = 1
        SET @sql = 'CREATE TABLE #filesql (filename VARCHAR(20));
                    BULK INSERT #filesql FROM ''Filelist.csv''
                    WITH ( DATA_SOURCE = ''BlobStorage'', FORMAT = ''CSV'', FIRSTROW=2, CODEPAGE=''65001'' );
                    SELECT * FROM #filesql';            
    ELSE
        SET @sql = 'CREATE TABLE #filesql (filename VARCHAR(20));
                    BULK INSERT #filesql FROM '''+@FileLocation+'\Filelist.csv''
                    WITH ( FORMAT = ''CSV'', FIRSTROW=2, CODEPAGE=''65001'' );
                    SELECT * FROM #filesql';         
   
    INSERT #filelist EXECUTE sp_executesql @sql;

    DECLARE filename_cursor CURSOR FOR   
    SELECT filename  
    FROM #filelist  
    ORDER BY filename;  

    
    PRINT '-------- Load Ordnance Survey Files --------';  
    DELETE FROM [dbo].[os_open_names];
  
    OPEN filename_cursor  
    BEGIN  
        FETCH NEXT FROM filename_cursor   
        INTO @filename  
          
        WHILE @@FETCH_STATUS = 0  
        BEGIN  
            PRINT '----- Data From filename: ' + @filename 

            IF @LoadBLOB = 1
                SET @sql = 'BULK INSERT [dbo].[os_open_names] FROM '''+@filename+'''
                            WITH ( DATA_SOURCE = ''BlobStorage'', FORMAT = ''CSV'', FIRSTROW=2, CODEPAGE=''65001'' )';            
            ELSE
                SET @sql = 'BULK INSERT [dbo].[os_open_names] FROM '''+@FileLocation+'\'+@filename+'''
                            WITH ( FORMAT = ''CSV'', FIRSTROW=2, CODEPAGE=''65001'' )';            

            EXECUTE sp_executesql @sql;
     
            FETCH NEXT FROM filename_cursor   
            INTO @filename  
         
        END   
        CLOSE filename_cursor;  
        DEALLOCATE filename_cursor; 

    END
END
GO



