-- Creating table 'tbl_AdvertisingType'
CREATE TABLE [dbo].[tbl_AdvertisingType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Code] nvarchar(256)  NOT NULL,
    [AdvertisingTypeCategoryID] int  NOT NULL
);
GO
-- Creating foreign key on [AdvertisingTypeCategoryID] in table 'tbl_AdvertisingType'
ALTER TABLE [dbo].[tbl_AdvertisingType]
ADD CONSTRAINT [FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory]
    FOREIGN KEY ([AdvertisingTypeCategoryID])
    REFERENCES [dbo].[tbl_AdvertisingTypeCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_AdvertisingType'
ALTER TABLE [dbo].[tbl_AdvertisingType]
ADD CONSTRAINT [FK_tbl_AdvertisingType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AdvertisingType'
ALTER TABLE [dbo].[tbl_AdvertisingType]
ADD CONSTRAINT [PK_tbl_AdvertisingType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory'
CREATE INDEX [IX_FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory]
ON [dbo].[tbl_AdvertisingType]
    ([AdvertisingTypeCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AdvertisingType_tbl_Sites]
ON [dbo].[tbl_AdvertisingType]
    ([SiteID]);