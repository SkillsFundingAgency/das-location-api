CREATE TABLE [os_open_names]
(id varchar(50) PRIMARY KEY,
names_uri varchar(500),
name1 varchar(500),
name1_lang varchar(500),
name2 varchar(500),
name2_lang varchar(500),
type varchar(500),
local_type varchar(500),
geometry_x varchar(500),
geometry_y varchar(500),
most_detail_view_res varchar(500),
least_detail_view_res varchar(500),
mbr_xmin varchar(500),
mbr_ymin varchar(500),
mbr_xmax varchar(500),
mbr_ymax varchar(500),
postcode_district varchar(500),
postcode_district_uri varchar(500),
populated_place varchar(500),
populated_place_uri varchar(500),
populated_place_type varchar(500),
district_borough varchar(500),
district_borough_uri varchar(500),
district_borough_type varchar(500),
county_unitary varchar(500),
county_unitary_uri varchar(500),
county_unitary_type varchar(500),
region varchar(500),
region_uri varchar(500),
country varchar(500),
country_uri varchar(500),
related_spatial_object varchar(500),
same_as_dbpedia varchar(500),
same_as_geonames varchar(500)
)
GO

CREATE INDEX [IX_os_open_names_name1] ON [dbo].[os_open_names] ([name1],[country]) INCLUDE ([type],[postcode_district],[district_borough],[county_unitary])
GO

