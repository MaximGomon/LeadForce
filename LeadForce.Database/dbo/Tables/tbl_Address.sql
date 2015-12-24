-- Creating table 'tbl_Address'
CREATE TABLE [dbo].[tbl_Address] (
    [ID] uniqueidentifier  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [CountryID] uniqueidentifier  NULL,
    [CityID] uniqueidentifier  NULL,
    [DistrictID] uniqueidentifier  NULL,
    [RegionID] uniqueidentifier  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Address'
ALTER TABLE [dbo].[tbl_Address]
ADD CONSTRAINT [PK_tbl_Address]
    PRIMARY KEY CLUSTERED ([ID] ASC);