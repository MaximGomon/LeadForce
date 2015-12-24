-- Creating table 'tbl_City'
CREATE TABLE [dbo].[tbl_City] (
    [ImportID] int  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ImportCountryID] int  NOT NULL,
    [Latitude] decimal(10,6)  NOT NULL,
    [Longitude] decimal(10,6)  NOT NULL,
    [ImportRegionID] int  NULL,
    [ImportDistrictID] int  NULL,
    [ID] uniqueidentifier  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL,
    [RegionID] uniqueidentifier  NOT NULL,
    [DistrictID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [CountryID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [FK_tbl_City_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [DistrictID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [FK_tbl_City_tbl_District]
    FOREIGN KEY ([DistrictID])
    REFERENCES [dbo].[tbl_District]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RegionID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [FK_tbl_City_tbl_Region]
    FOREIGN KEY ([RegionID])
    REFERENCES [dbo].[tbl_Region]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [PK_tbl_City]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_City_tbl_Country'
CREATE INDEX [IX_FK_tbl_City_tbl_Country]
ON [dbo].[tbl_City]
    ([CountryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_City_tbl_District'
CREATE INDEX [IX_FK_tbl_City_tbl_District]
ON [dbo].[tbl_City]
    ([DistrictID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_City_tbl_Region'
CREATE INDEX [IX_FK_tbl_City_tbl_Region]
ON [dbo].[tbl_City]
    ([RegionID]);