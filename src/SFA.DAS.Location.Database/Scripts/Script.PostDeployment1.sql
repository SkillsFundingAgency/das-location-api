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

Truncate table PostcodeOutcode

/*  Insert Range of Birmingham Outcodes */
Insert into PostcodeOutcode
    VALUES
('B1','Birmingham','Birmingham'),
('B2','Birmingham','Birmingham'),
('B3','Birmingham','Birmingham'),
('B4','Birmingham','Birmingham'),
('B5','Birmingham','Birmingham'),
('B6','Birmingham','Birmingham'),
('B7','Birmingham','Birmingham'),
('B8','Birmingham','Birmingham'),
('B9','Birmingham','Birmingham'),
('B10','Birmingham','Birmingham'),
('B11','Birmingham','Birmingham'),
('B12','Birmingham','Birmingham'),
('B13','Birmingham','Birmingham'),
('B14','Birmingham','Birmingham'),
('B15','Birmingham','Birmingham'),
('B16','Birmingham','Birmingham'),
('B17','Birmingham','Birmingham'),
('B18','Birmingham','Birmingham'),
('B19','Birmingham','Birmingham'),
('B20','Birmingham','Birmingham'),
('B21','Birmingham','Birmingham'),
('B23','Birmingham','Birmingham'),
('B24','Birmingham','Birmingham'),
('B25','Birmingham','Birmingham'),
('B26','Birmingham','Birmingham'),
('B27','Birmingham','Birmingham'),
('B28','Birmingham','Birmingham'),
('B29','Birmingham','Birmingham'),
('B30','Birmingham','Birmingham'),
('B31','Birmingham','Birmingham'),
('B32','Birmingham','Birmingham'),
('B33','Birmingham','Birmingham'),
('B34','Birmingham','Birmingham'),
('B35','Birmingham','Birmingham'),
('B36','Birmingham','Birmingham'),
('B37','Birmingham','Birmingham'),
('B38','Birmingham','Birmingham'),
('B40','Birmingham','Birmingham'),
('B42','Birmingham','Birmingham'),
('B43','Birmingham','Birmingham'),
('B44','Birmingham','Birmingham'),
('B45','Birmingham','Birmingham'),
('B46','Birmingham','Birmingham'),
('B47','Birmingham','Birmingham'),
('B48','Birmingham','Birmingham'),
('B99','Birmingham','Birmingham'),
('B49','Birmingham','Alcester'),
('B50','Birmingham','Alcester'),
('B60','Birmingham','Bromsgrove'),
('B61','Birmingham','Bromsgrove'),
('B62','Birmingham','Halesowen'),
('B63','Birmingham','Halesowen'),
('B66','Birmingham','Smethwick'),
('B67','Birmingham','Smethwick'),
('B70','Birmingham','West Bromwich'),
('B71','Birmingham','West Bromwich'),
('B77','Birmingham','TAMWORTH'),
('B78','Birmingham','TAMWORTH'),
('B79','Birmingham','TAMWORTH'),
('B90','Birmingham','Solihull'),
('B91','Birmingham','Solihull'),
('B92','Birmingham','Solihull'),
('B93','Birmingham','Solihull'),
('B94','Birmingham','Solihull')


/*  Insert Range of Coventry Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('CV1','Coventry','Coventry'),
    ('CV2','Coventry','Coventry'),
    ('CV3','Coventry','Coventry'),
    ('CV4','Coventry','Coventry'),
    ('CV5','Coventry','Coventry'),
    ('CV6','Coventry','Coventry'),
    ('CV7','Coventry','Coventry'),
    ('CV8','Coventry','Coventry'),
    ('CV10','Coventry','Nuneaton'),
    ('CV11','Coventry','Nuneaton'),
    ('CV12','Coventry','Bedworth'),
    ('CV13','Coventry','Nuneaton'),
    ('CV21','Coventry','Rugby'),
    ('CV22','Coventry','Rugby'),
    ('CV23','Coventry','Rugby'),
    ('CV31','Coventry','Leamington Spa'),
    ('CV32','Coventry','Leamington Spa'),
    ('CV33','Coventry','Leamington Spa'),
    ('CV34','Coventry','Warwick'),
    ('CV35','Coventry','Warwick')

/*  Insert Range of East London Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('E1','East London','London'),
    ('E1W','East London','London'),
    ('E2','East London','London'),
    ('E3','East London','London'),
    ('E4','East London','London'),
    ('E5','East London','London'),
    ('E6','East London','London'),
    ('E7','East London','London'),
    ('E8','East London','London'),
    ('E9','East London','London'),
    ('E10','East London','London'),
    ('E11','East London','London'),
    ('E12','East London','London'),
    ('E13','East London','London'),
    ('E14','East London','London'),
    ('E15','East London','London'),
    ('E16','East London','London'),
    ('E17','East London','London'),
    ('E18','East London','London'),
    ('E20','East London','London')


/*  Insert Range of East Central London Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('EC1A','East Central London','London'),
    ('EC1M','East Central London','London'),
    ('EC1N','East Central London','London'),
    ('EC1P','East Central London','London'),
    ('EC1V','East Central London','London'),
    ('EC1Y','East Central London','London'),
    ('EC2A','East Central London','London'),
    ('EC2M','East Central London','London'),
    ('EC2N','East Central London','London'),
    ('EC2P','East Central London','London'),
    ('EC2V','East Central London','London'),
    ('EC2Y','East Central London','London'),
    ('EC3A','East Central London','London'),
    ('EC3M','East Central London','London'),
    ('EC3N','East Central London','London'),
    ('EC3P','East Central London','London'),
    ('EC3V','East Central London','London'),
    ('EC4A','East Central London','London'),
    ('EC4M','East Central London','London'),
    ('EC4N','East Central London','London'),
    ('EC4P','East Central London','London'),
    ('EC4V','East Central London','London'),
    ('EC4Y','East Central London','London')


/*  Insert Range of Leeds Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('LS1','Leeds','Leeds'),
    ('LS2','Leeds','Leeds'),
    ('LS3','Leeds','Leeds'),
    ('LS4','Leeds','Leeds'),
    ('LS5','Leeds','Leeds'),
    ('LS6','Leeds','Leeds'),
    ('LS7','Leeds','Leeds'),
    ('LS8','Leeds','Leeds'),
    ('LS9','Leeds','Leeds'),
    ('LS10','Leeds','Leeds'),
    ('LS11','Leeds','Leeds'),
    ('LS12','Leeds','Leeds'),
    ('LS13','Leeds','Leeds'),
    ('LS14','Leeds','Leeds'),
    ('LS15','Leeds','Leeds'),
    ('LS16','Leeds','Leeds'),
    ('LS17','Leeds','Leeds'),
    ('LS18','Leeds','Leeds'),
    ('LS19','Leeds','Leeds'),
    ('LS20','Leeds','Leeds'),
    ('LS21','Leeds','Otley'),
    ('LS22','Leeds','Wetherby'),
    ('LS23','Leeds','Wetherby'),
    ('LS24','Leeds','Tadcaster'),
    ('LS25','Leeds','Leeds'),
    ('LS26','Leeds','Leeds'),
    ('LS27','Leeds','Leeds'),
    ('LS28','Leeds','Pudsey'),
    ('LS29','Leeds','Ilkley')

/*  Insert Range of Manchester Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('M1','Manchester','Manchester'),
    ('M2','Manchester','Manchester'),
    ('M3','Manchester','Manchester'),
    ('M4','Manchester','Manchester'),
    ('M5','Manchester','Salford'),
    ('M6','Manchester','Salford'),
    ('M7','Manchester','Salford'),
    ('M8','Manchester','Manchester'),
    ('M9','Manchester','Manchester'),
    ('M10','Manchester','Manchester'),
    ('M11','Manchester','Manchester'),
    ('M12','Manchester','Manchester'),
    ('M13','Manchester','Manchester'),
    ('M14','Manchester','Manchester'),
    ('M15','Manchester','Manchester'),
    ('M16','Manchester','Manchester'),
    ('M17','Manchester','Manchester'),
    ('M18','Manchester','Manchester'),
    ('M19','Manchester','Manchester'),
    ('M20','Manchester','Manchester'),
    ('M21','Manchester','Manchester'),
    ('M22','Manchester','Manchester'),
    ('M23','Manchester','Manchester'),
    ('M24','Manchester','Manchester'),
    ('M25','Manchester','Manchester'),
    ('M26','Manchester','Manchester'),
    ('M27','Manchester','Manchester'),
    ('M28','Manchester','Manchester'),
    ('M29','Manchester','Manchester'),
    ('M30','Manchester','Manchester'),
    ('M31','Manchester','Manchester'),
    ('M32','Manchester','Manchester'),
    ('M33','Manchester','Sale'),
    ('M34','Manchester','Manchester'),
    ('M35','Manchester','Manchester'),
    ('M36','Manchester','Manchester'),
    ('M37','Manchester','Manchester'),
    ('M38','Manchester','Manchester'),
    ('M39','Manchester','Manchester'),
    ('M40','Manchester','Manchester'),
    ('M41','Manchester','Manchester'),
    ('M42','Manchester','Manchester'),
    ('M43','Manchester','Manchester'),
    ('M44','Manchester','Manchester'),
    ('M45','Manchester','Manchester'),
    ('M46','Manchester','Manchester'),
    ('M50','Manchester','Salford'),
    ('M60','Manchester','Salford')

/*  Insert Range of North London Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('N1','North London','London'),
    ('N1C','North London','London'),
    ('N2','North London','London'),
    ('N3','North London','London'),
    ('N4','North London','London'),
    ('N5','North London','London'),
    ('N6','North London','London'),
    ('N7','North London','London'),
    ('N8','North London','London'),
    ('N9','North London','London'),
    ('N10','North London','London'),
    ('N11','North London','London'),
    ('N12','North London','London'),
    ('N13','North London','London'),
    ('N14','North London','London'),
    ('N15','North London','London'),
    ('N16','North London','London'),
    ('N17','North London','London'),
    ('N18','North London','London'),
    ('N19','North London','London'),
    ('N20','North London','London'),
    ('N21','North London','London'),
    ('N22','North London','London')


/*  Insert Range of West London Outcodes */

  Insert into PostcodeOutcode
    VALUES
    ('W1A','West London','London'),
    ('W1B','West London','London'),
    ('W1C','West London','London'),
    ('W1D','West London','London'),
    ('W1F','West London','London'),
    ('W1G','West London','London'),
    ('W1H','West London','London'),
    ('W1J','West London','London'),
    ('W1K','West London','London'),
    ('W1S','West London','London'),
    ('W1T','West London','London'),
    ('W1U','West London','London'),
    ('W1W','West London','London'),
    ('W1','West London','London'),
    ('W2','West London','London'),
    ('W3','West London','London'),
    ('W4','West London','London'),
    ('W5','West London','London'),
    ('W6','West London','London'),
    ('W7','West London','London'),
    ('W8','West London','London'),
    ('W9','West London','London'),
    ('W10','West London','London'),
    ('W11','West London','London'),
    ('W12','West London','London'),
    ('W13','West London','London'),
    ('W14','West London','London')
