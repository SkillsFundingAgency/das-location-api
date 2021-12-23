/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- ONS data set merge with OS places data

-- load ONS data
:r .\ONS_FileLoad.sql

-- setup mapping for multi-local authority areas.
:r .\Setup_Local_Authority.sql

-- setup locations table
:r .\Setup_Locations.sql

-- setup PostcodeOutcodes (sample)
:r .\Setup_PostcodeOutcodes.sql

-- load OS data
--:r .\FileLoad.sql


