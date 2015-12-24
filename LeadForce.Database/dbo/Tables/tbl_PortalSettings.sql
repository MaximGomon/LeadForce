-- Creating table 'tbl_PortalSettings'
CREATE TABLE [dbo].[tbl_PortalSettings] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Domain] nvarchar(256)  NULL,
    [Logo] nvarchar(256)  NULL,
    [CompanyMessage] nvarchar(1024)  NULL,
    [HeaderTemplate] nvarchar(4000)  NULL,
    [Title] nvarchar(256)  NOT NULL,
    [WelcomeMessage] nvarchar(256)  NOT NULL,
    [MainMenuBackground] nvarchar(7)  NULL,
    [BlockTitleBackground] nvarchar(7)  NULL,
    [FacebookProfile] nvarchar(512)  NULL,
    [VkontakteProfile] nvarchar(512)  NULL,
    [TwitterProfile] nvarchar(512)  NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_PortalSettings'
ALTER TABLE [dbo].[tbl_PortalSettings]
ADD CONSTRAINT [FK_tbl_PortalSettings_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PortalSettings'
ALTER TABLE [dbo].[tbl_PortalSettings]
ADD CONSTRAINT [PK_tbl_PortalSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PortalSettings_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PortalSettings_tbl_Sites]
ON [dbo].[tbl_PortalSettings]
    ([SiteID]);