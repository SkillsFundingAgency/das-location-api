﻿-- Setup or update the local authority list
-- Any changes made to the insert below, by removing value rows and 
-- by changing the Local Authority or Unitary Council values will 
-- result in an change/insert/delete on the [dbo].[Local_Authority] table

CREATE TABLE #Local_Authority ([LocalAuthority] VARCHAR(256), [UnitaryCouncil] VARCHAR(256));

-- 
INSERT INTO #Local_Authority
VALUES 
('Adur','West Sussex'),
('Allerdale','Cumbria'),
('Amber Valley','Derbyshire'),
('Arun','West Sussex'),
('Ashfield','Nottinghamshire'),
('Ashford','Kent'),
('Babergh','Suffolk'),
('Barking and Dagenham','Greater London'),
('Barnet','Greater London'),
('Barnsley',null),
('Barrow-in-Furness','Cumbria'),
('Basildon','Essex'),
('Basingstoke and Deane','Hampshire'),
('Bassetlaw','Nottinghamshire'),
('Bath and North East Somerset','Bath and North East Somerset'),
('Bedford','Bedford'),
('Bexley','Greater London'),
('Birmingham',null),
('Blaby','Leicestershire'),
('Blackburn with Darwen','Blackburn with Darwen'),
('Blackpool','Blackpool'),
('Bolsover','Derbyshire'),
('Bolton',null),
('Boston','Lincolnshire'),
('Bournemouth, Christchurch and Poole','Bournemouth, Christchurch and Poole'),
('Bracknell Forest','Bracknell Forest'),
('Bradford',null),
('Braintree','Essex'),
('Breckland','Norfolk'),
('Brent','Greater London'),
('Brentwood','Essex'),
('Brighton and Hove','The City of Brighton and Hove'),
('Bristol, City of','City of Bristol'),
('Broadland','Norfolk'),
('Bromley','Greater London'),
('Bromsgrove','Worcestershire'),
('Broxbourne','Hertfordshire'),
('Broxtowe','Nottinghamshire'),
('Buckinghamshire','Buckinghamshire'),
('Burnley','Lancashire'),
('Bury',null),
('Calderdale',null),
('Cambridge','Cambridgeshire'),
('Camden','Greater London'),
('Cannock Chase','Staffordshire'),
('Canterbury','Kent'),
('Carlisle','Cumbria'),
('Castle Point','Essex'),
('Central Bedfordshire','Central Bedfordshire'),
('Charnwood','Leicestershire'),
('Chelmsford','Essex'),
('Cheltenham','Gloucestershire'),
('Cherwell','Oxfordshire'),
('Cheshire East','Cheshire East'),
('Cheshire West and Chester','Cheshire West and Chester'),
('Chesterfield','Derbyshire'),
('Chichester','West Sussex'),
('Chorley','Lancashire'),
('City of London','Greater London'),
('Colchester','Essex'),
('Copeland','Cumbria'),
('Cornwall','Cornwall'),
('Cotswold','Gloucestershire'),
('County Durham','County Durham'),
('Coventry',null),
('Craven','North Yorkshire'),
('Crawley','West Sussex'),
('Croydon','Greater London'),
('Cumbria','Cumbria'),
('Dacorum','Hertfordshire'),
('Darlington','Darlington'),
('Dartford','Kent'),
('Derby','City of Derby'),
('Derbyshire Dales','Derbyshire'),
('Devon','Devon'),
('Doncaster',null),
('Dorset','Dorset'),
('Dover','Kent'),
('Dudley',null),
('Ealing','Greater London'),
('East Cambridgeshire','Cambridgeshire'),
('East Devon','Devon'),
('East Hampshire','Hampshire'),
('East Hertfordshire','Hertfordshire'),
('East Lindsey','Lincolnshire'),
('East Riding of Yorkshire','East Riding of Yorkshire'),
('East Staffordshire','Staffordshire'),
('East Suffolk','Suffolk'),
('East Sussex','East Sussex'),
('Eastbourne','East Sussex'),
('Eastleigh','Hampshire'),
('Eden','Cumbria'),
('Elmbridge','Surrey'),
('Enfield','Greater London'),
('Epping Forest','Essex'),
('Epsom and Ewell','Surrey'),
('Erewash','Derbyshire'),
('Essex','Essex'),
('Exeter','Devon'),
('Fareham','Hampshire'),
('Fenland','Cambridgeshire'),
('Folkestone and Hythe','Kent'),
('Forest of Dean','Gloucestershire'),
('Fylde','Lancashire'),
('Gateshead',null),
('Gedling','Nottinghamshire'),
('Gloucester','Gloucestershire'),
('Gosport','Hampshire'),
('Gravesham','Kent'),
('Great Yarmouth','Norfolk'),
('Greenwich','Greater London'),
('Guildford','Surrey'),
('Hackney','Greater London'),
('Halton','Halton'),
('Hambleton','North Yorkshire'),
('Hammersmith and Fulham','Greater London'),
('Hampshire','Hampshire'),
('Harborough','Leicestershire'),
('Haringey','Greater London'),
('Harlow','Essex'),
('Harrogate','North Yorkshire'),
('Harrow','Greater London'),
('Hart','Hampshire'),
('Hartlepool','Hartlepool'),
('Hastings','East Sussex'),
('Havant','Hampshire'),
('Havering','Greater London'),
('Herefordshire, County of','County of Herefordshire'),
('Hertsmere','Hertfordshire'),
('High Peak','Derbyshire'),
('Hillingdon','Greater London'),
('Hinckley and Bosworth','Leicestershire'),
('Horsham','West Sussex'),
('Hounslow','Greater London'),
('Huntingdonshire','Cambridgeshire'),
('Hyndburn','Lancashire'),
('Ipswich','Suffolk'),
('Isle of Wight','Isle of Wight'),
('Isles of Scilly','Isles of Scilly'),
('Islington','Greater London'),
('Kensington and Chelsea','Greater London'),
('Kent','Kent'),
('King''s Lynn and West Norfolk','Norfolk'),
('Kingston upon Hull, City of','City of Kingston upon Hull'),
('Kingston upon Thames','Greater London'),
('Kirklees',null),
('Knowsley',null),
('Lambeth','Greater London'),
('Lancashire','Lancashire'),
('Lancaster','Lancashire'),
('Leeds',null),
('Leicester','City of Leicester'),
('Lewes','East Sussex'),
('Lewisham','Greater London'),
('Lichfield','Staffordshire'),
('Lincoln','Lincolnshire'),
('Lincolnshire','Lincolnshire'),
('Liverpool',null),
('Luton','Luton'),
('Maidstone','Kent'),
('Maldon','Essex'),
('Malvern Hills','Worcestershire'),
('Manchester',null),
('Mansfield','Nottinghamshire'),
('Medway','Medway'),
('Melton','Leicestershire'),
('Mendip','Somerset'),
('Merton','Greater London'),
('Mid Devon','Devon'),
('Mid Suffolk','Suffolk'),
('Mid Sussex','West Sussex'),
('Middlesbrough','Middlesbrough'),
('Milton Keynes','Milton Keynes'),
('Mole Valley','Surrey'),
('New Forest','Hampshire'),
('Newark and Sherwood','Nottinghamshire'),
('Newcastle upon Tyne',null),
('Newcastle-under-Lyme','Staffordshire'),
('Newham','Greater London'),
('Norfolk','Norfolk'),
('North Devon','Devon'),
('North East Derbyshire','Derbyshire'),
('North East Lincolnshire','North East Lincolnshire'),
('North Hertfordshire','Hertfordshire'),
('North Kesteven','Lincolnshire'),
('North Lincolnshire','North Lincolnshire'),
('North Norfolk','Norfolk'),
('North Northamptonshire','North Northamptonshire'),
('North Somerset','North Somerset'),
('North Tyneside',null),
('North Warwickshire','Warwickshire'),
('North West Leicestershire','Leicestershire'),
('North Yorkshire','North Yorkshire'),
('Northumberland','Northumberland'),
('Norwich','Norfolk'),
('Nottingham','City of Nottingham'),
('Nuneaton and Bedworth','Warwickshire'),
('Oadby and Wigston','Leicestershire'),
('Oldham',null),
('Oxford','Oxfordshire'),
('Pendle','Lancashire'),
('Peterborough','City of Peterborough'),
('Plymouth','City of Plymouth'),
('Portsmouth','City of Portsmouth'),
('Preston','Lancashire'),
('Reading','Reading'),
('Redbridge','Greater London'),
('Redcar and Cleveland','Redcar and Cleveland'),
('Redditch','Worcestershire'),
('Reigate and Banstead','Surrey'),
('Ribble Valley','Lancashire'),
('Richmond upon Thames','Greater London'),
('Richmondshire','North Yorkshire'),
('Rochdale',null),
('Rochford','Essex'),
('Rossendale','Lancashire'),
('Rother','East Sussex'),
('Rotherham',null),
('Rugby','Warwickshire'),
('Runnymede','Surrey'),
('Rushcliffe','Nottinghamshire'),
('Rushmoor','Hampshire'),
('Rutland','Rutland'),
('Ryedale','North Yorkshire'),
('Salford',null),
('Sandwell',null),
('Scarborough','North Yorkshire'),
('Sedgemoor','Somerset'),
('Sefton',null),
('Selby','North Yorkshire'),
('Sevenoaks','Kent'),
('Sheffield',null),
('Shropshire','Shropshire'),
('Slough','Slough'),
('Solihull',null),
('Somerset','Somerset'),
('Somerset West and Taunton','Somerset'),
('South Cambridgeshire','Cambridgeshire'),
('South Derbyshire','Derbyshire'),
('South Gloucestershire','South Gloucestershire'),
('South Hams','Devon'),
('South Holland','Lincolnshire'),
('South Kesteven','Lincolnshire'),
('South Lakeland','Cumbria'),
('South Norfolk','Norfolk'),
('South Oxfordshire','Oxfordshire'),
('South Ribble','Lancashire'),
('South Somerset','Somerset'),
('South Staffordshire','Staffordshire'),
('South Tyneside',null),
('Southampton','City of Southampton'),
('Southend-on-Sea','Southend-on-Sea'),
('Southwark','Greater London'),
('Spelthorne','Surrey'),
('St Albans','Hertfordshire'),
('St. Helens',null),
('Stafford','Staffordshire'),
('Staffordshire Moorlands','Staffordshire'),
('Stevenage','Hertfordshire'),
('Stockport',null),
('Stockton-on-Tees','Stockton-on-Tees'),
('Stoke-on-Trent','City of Stoke-on-Trent'),
('Stratford-on-Avon','Warwickshire'),
('Stroud','Gloucestershire'),
('Suffolk','Suffolk'),
('Sunderland',null),
('Surrey Heath','Surrey'),
('Sutton','Greater London'),
('Swale','Kent'),
('Swindon','Swindon'),
('Tameside',null),
('Tamworth','Staffordshire'),
('Tandridge','Surrey'),
('Teignbridge','Devon'),
('Telford and Wrekin','Telford and Wrekin'),
('Tendring','Essex'),
('Test Valley','Hampshire'),
('Tewkesbury','Gloucestershire'),
('Thanet','Kent'),
('Three Rivers','Hertfordshire'),
('Thurrock','Thurrock'),
('Tonbridge and Malling','Kent'),
('Torbay','Torbay'),
('Torridge','Devon'),
('Tower Hamlets','Greater London'),
('Trafford',null),
('Tunbridge Wells','Kent'),
('Uttlesford','Essex'),
('Vale of White Horse','Oxfordshire'),
('Wakefield',null),
('Walsall',null),
('Waltham Forest','Greater London'),
('Wandsworth','Greater London'),
('Warrington','Warrington'),
('Warwick','Warwickshire'),
('Watford','Hertfordshire'),
('Waverley','Surrey'),
('Wealden','East Sussex'),
('Welwyn Hatfield','Hertfordshire'),
('West Berkshire','West Berkshire'),
('West Devon','Devon'),
('West Lancashire','Lancashire'),
('West Lindsey','Lincolnshire'),
('West Northamptonshire','West Northamptonshire'),
('West Oxfordshire','Oxfordshire'),
('West Suffolk','Suffolk'),
('West Sussex','West Sussex'),
('Westminster','Greater London'),
('Wigan',null),
('Wiltshire','Wiltshire'),
('Winchester','Hampshire'),
('Windsor and Maidenhead','Windsor and Maidenhead'),
('Wirral',null),
('Woking','Surrey'),
('Wokingham','Wokingham'),
('Wolverhampton',null),
('Worcester','Worcestershire'),
('Worthing','West Sussex'),
('Wychavon','Worcestershire'),
('Wyre','Lancashire'),
('Wyre Forest','Worcestershire'),
('York','York');

GO

BEGIN
    DECLARE @sqlcount int;
    SELECT @sqlcount = COUNT(*) FROM #Local_Authority;
    PRINT '-------- Loaded '+CONVERT(varchar,@sqlcount)+' records in to temp Local_Authority table ----------';

    IF  @sqlcount > 0
    BEGIN
        -- synch the data records that have changed
        MERGE INTO [dbo].[Local_Authority] main
        USING #Local_Authority temp
        ON main.LocalAuthority = temp.LocalAuthority
        --When records are matched, update the records if there is any change
        WHEN MATCHED AND main.UnitaryCouncil != temp.UnitaryCouncil THEN 
            UPDATE SET main.UnitaryCouncil = temp.UnitaryCouncil
        --When no records are matched, insert the temp records to [Local_Authority] table
        WHEN NOT MATCHED THEN
            INSERT (LocalAuthority, UnitaryCouncil) VALUES (temp.LocalAuthority, temp.UnitaryCouncil)
        --When there is a row that exists in main and same record does not exist in temp then delete this record target
        WHEN NOT MATCHED BY SOURCE THEN
            DELETE; 

        PRINT '-------- Changed records updated in Local_Authority table ----------';

    END
END;
GO
