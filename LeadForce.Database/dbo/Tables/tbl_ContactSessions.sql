-- Creating table 'tbl_ContactSessions'
CREATE TABLE [dbo].[tbl_ContactSessions] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SessionDate] datetime  NOT NULL,
    [RefferURL] nvarchar(2000)  NULL,
    [UserIP] varchar(15)  NULL,
    [BrowserID] uniqueidentifier  NULL,
    [OperatingSystemID] uniqueidentifier  NULL,
    [ResolutionID] uniqueidentifier  NULL,
    [MobileDeviceID] uniqueidentifier  NULL,
    [UserAgent] nvarchar(500)  NULL,
    [UserSessionNumber] int  NOT NULL,
    [EnterPointUrl] nvarchar(2000)  NULL,
    [Keywords] nvarchar(2000)  NULL,
    [Content] nvarchar(2000)  NULL,
    [ImportCityID] int  NULL,
    [ImportCountryID] int  NULL,
    [CityID] uniqueidentifier  NULL,
    [CountryID] uniqueidentifier  NULL,
    [RefferID] uniqueidentifier  NULL,
    [AdvertisingTypeID] uniqueidentifier  NULL,
    [AdvertisingPlatformID] uniqueidentifier  NULL,
    [AdvertisingCampaignID] uniqueidentifier  NULL,
    [CameFromURL] nvarchar(2000)  NULL
);
GO
-- Creating foreign key on [AdvertisingCampaignID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingCampaign]
    FOREIGN KEY ([AdvertisingCampaignID])
    REFERENCES [dbo].[tbl_AdvertisingCampaign]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AdvertisingPlatformID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingPlatform]
    FOREIGN KEY ([AdvertisingPlatformID])
    REFERENCES [dbo].[tbl_AdvertisingPlatform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AdvertisingTypeID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingType]
    FOREIGN KEY ([AdvertisingTypeID])
    REFERENCES [dbo].[tbl_AdvertisingType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [BrowserID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Browsers]
    FOREIGN KEY ([BrowserID])
    REFERENCES [dbo].[tbl_Browsers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CityID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_City]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[tbl_City]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CountryID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [MobileDeviceID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_MobileDevices]
    FOREIGN KEY ([MobileDeviceID])
    REFERENCES [dbo].[tbl_MobileDevices]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OperatingSystemID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_OperatingSystems]
    FOREIGN KEY ([OperatingSystemID])
    REFERENCES [dbo].[tbl_OperatingSystems]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResolutionID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Resolutions]
    FOREIGN KEY ([ResolutionID])
    REFERENCES [dbo].[tbl_Resolutions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [PK_tbl_ContactSessions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_AdvertisingCampaign'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_AdvertisingCampaign]
ON [dbo].[tbl_ContactSessions]
    ([AdvertisingCampaignID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_AdvertisingPlatform'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_AdvertisingPlatform]
ON [dbo].[tbl_ContactSessions]
    ([AdvertisingPlatformID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_AdvertisingType'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_AdvertisingType]
ON [dbo].[tbl_ContactSessions]
    ([AdvertisingTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Browsers'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Browsers]
ON [dbo].[tbl_ContactSessions]
    ([BrowserID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_City'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_City]
ON [dbo].[tbl_ContactSessions]
    ([CityID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Contact]
ON [dbo].[tbl_ContactSessions]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Country'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Country]
ON [dbo].[tbl_ContactSessions]
    ([CountryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_MobileDevices'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_MobileDevices]
ON [dbo].[tbl_ContactSessions]
    ([MobileDeviceID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_OperatingSystems'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_OperatingSystems]
ON [dbo].[tbl_ContactSessions]
    ([OperatingSystemID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Resolutions'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Resolutions]
ON [dbo].[tbl_ContactSessions]
    ([ResolutionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Sites]
ON [dbo].[tbl_ContactSessions]
    ([SiteID]);