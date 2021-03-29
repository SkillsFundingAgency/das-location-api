# das-location-api

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status/das-location-api?repoName=SkillsFundingAgency%2Fdas-location-api&branchName=master)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2255&repoName=SkillsFundingAgency%2Fdas-location-api&branchName=master)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-location-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=SkillsFundingAgency_das-location-api)

## Requirements

- DotNet Core 3.1 and any supported IDE for DEV running.
- *If you are not wishing to run the in memory database then*
- SQL Server database.
- Azure Storage Account

## About

das-location-api represents the inner api definition for location based information, with data taken from The Office for National Statistics and [Postcodes IO](https://postcodes.io/). 
The API creates a local copy of the data for querying over, allowing you to search for locations by name or postcode

## Local running

**Do not run in IISExpress**

### In memory database
It is possible to run the whole of the API using the InMemory database. To do this the environment variable in appsettings.json should be set to **DEV**. 
Once done, start the application as normal, then run the ```ops/dataload``` operation in swagger. You will then be able to query the API
as per the operations listed in swagger.

### SQL Server database
You are able to run the API by doing the following:

* Run the database deployment publish command to create the database ```SFA.DAS.Location``` or create the database manually and run in the table creation scripts
* In your Azure Storage Account, create a table called Configuration and Add the following
```
ParitionKey: LOCAL
RowKey: SFA.DAS.Location.Api_1.0
Data: {"LocationApiConfiguration":{"ConnectionString":"DBCONNECTIONSTRING"}}
```

* Start the api project ```SFA.DAS.Location.Api```

Select the operations V1 definition and choose the dataload action

```POST /ops/dataload```

will load data ONS for locations into the local database.

Starting the API will then show the swagger definition with the available operations.