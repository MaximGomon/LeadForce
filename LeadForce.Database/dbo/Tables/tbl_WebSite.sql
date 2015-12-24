-- Creating table 'tbl_WebSite'
CREATE TABLE [dbo].[tbl_WebSite] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(512)  NOT NULL,
    [Description] nvarchar(2048)  NULL,
    [SiteDomainID] uniqueidentifier  NULL,
    [FavIcon] nvarchar(50)  NULL
);
GO
-- Creating foreign key on [SiteDomainID] in table 'tbl_WebSite'
ALTER TABLE [dbo].[tbl_WebSite]
ADD CONSTRAINT [FK_tbl_WebSite_tbl_SiteDomain]
    FOREIGN KEY ([SiteDomainID])
    REFERENCES [dbo].[tbl_SiteDomain]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_WebSite'
ALTER TABLE [dbo].[tbl_WebSite]
ADD CONSTRAINT [FK_tbl_WebSite_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WebSite'
ALTER TABLE [dbo].[tbl_WebSite]
ADD CONSTRAINT [PK_tbl_WebSite]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSite_tbl_SiteDomain'
CREATE INDEX [IX_FK_tbl_WebSite_tbl_SiteDomain]
ON [dbo].[tbl_WebSite]
    ([SiteDomainID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSite_tbl_Sites'
CREATE INDEX [IX_FK_tbl_WebSite_tbl_Sites]
ON [dbo].[tbl_WebSite]
    ([SiteID]);