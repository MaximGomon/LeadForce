-- Creating table 'tbl_WebSitePageExternalResource'
CREATE TABLE [dbo].[tbl_WebSitePageExternalResource] (
    [tbl_ExternalResource_ID] uniqueidentifier  NOT NULL,
    [tbl_WebSitePage_ID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [tbl_ExternalResource_ID] in table 'tbl_WebSitePageExternalResource'
ALTER TABLE [dbo].[tbl_WebSitePageExternalResource]
ADD CONSTRAINT [FK_tbl_WebSitePageExternalResource_tbl_ExternalResource]
    FOREIGN KEY ([tbl_ExternalResource_ID])
    REFERENCES [dbo].[tbl_ExternalResource]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [tbl_WebSitePage_ID] in table 'tbl_WebSitePageExternalResource'
ALTER TABLE [dbo].[tbl_WebSitePageExternalResource]
ADD CONSTRAINT [FK_tbl_WebSitePageExternalResource_tbl_WebSitePage]
    FOREIGN KEY ([tbl_WebSitePage_ID])
    REFERENCES [dbo].[tbl_WebSitePage]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [tbl_ExternalResource_ID], [tbl_WebSitePage_ID] in table 'tbl_WebSitePageExternalResource'
ALTER TABLE [dbo].[tbl_WebSitePageExternalResource]
ADD CONSTRAINT [PK_tbl_WebSitePageExternalResource]
    PRIMARY KEY CLUSTERED ([tbl_ExternalResource_ID], [tbl_WebSitePage_ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSitePageExternalResource_tbl_WebSitePage'
CREATE INDEX [IX_FK_tbl_WebSitePageExternalResource_tbl_WebSitePage]
ON [dbo].[tbl_WebSitePageExternalResource]
    ([tbl_WebSitePage_ID]);