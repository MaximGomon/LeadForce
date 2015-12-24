-- Creating table 'tbl_AdvertisingCampaign'
CREATE TABLE [dbo].[tbl_AdvertisingCampaign] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Code] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_AdvertisingCampaign'
ALTER TABLE [dbo].[tbl_AdvertisingCampaign]
ADD CONSTRAINT [FK_tbl_AdvertisingCampaign_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AdvertisingCampaign'
ALTER TABLE [dbo].[tbl_AdvertisingCampaign]
ADD CONSTRAINT [PK_tbl_AdvertisingCampaign]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingCampaign_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AdvertisingCampaign_tbl_Sites]
ON [dbo].[tbl_AdvertisingCampaign]
    ([SiteID]);