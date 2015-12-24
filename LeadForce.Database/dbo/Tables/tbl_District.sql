-- Creating table 'tbl_District'
CREATE TABLE [dbo].[tbl_District] (
    [ImportID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ImportCountryID] int  NULL,
    [ID] uniqueidentifier  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL,
    [RegionID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [CountryID] in table 'tbl_District'
ALTER TABLE [dbo].[tbl_District]
ADD CONSTRAINT [FK_tbl_District_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RegionID] in table 'tbl_District'
ALTER TABLE [dbo].[tbl_District]
ADD CONSTRAINT [FK_tbl_District_tbl_Region]
    FOREIGN KEY ([RegionID])
    REFERENCES [dbo].[tbl_Region]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_District'
ALTER TABLE [dbo].[tbl_District]
ADD CONSTRAINT [PK_tbl_District]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_District_tbl_Country'
CREATE INDEX [IX_FK_tbl_District_tbl_Country]
ON [dbo].[tbl_District]
    ([CountryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_District_tbl_Region'
CREATE INDEX [IX_FK_tbl_District_tbl_Region]
ON [dbo].[tbl_District]
    ([RegionID]);