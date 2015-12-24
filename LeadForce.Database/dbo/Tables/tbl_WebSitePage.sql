-- Creating table 'tbl_WebSitePage'
CREATE TABLE [dbo].[tbl_WebSitePage] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Url] nvarchar(1024)  NULL,
    [WebSiteElementStatusID] int  NOT NULL,
    [MetaTitle] nvarchar(256)  NOT NULL,
    [MetaKeywords] nvarchar(2048)  NULL,
    [MetaDescription] nvarchar(2048)  NULL,
    [Body] nvarchar(max)  NULL,
    [IsHomePage] bit  NOT NULL,
    [WebSiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [WebSiteID] in table 'tbl_WebSitePage'
ALTER TABLE [dbo].[tbl_WebSitePage]
ADD CONSTRAINT [FK_tbl_WebSitePage_tbl_WebSite]
    FOREIGN KEY ([WebSiteID])
    REFERENCES [dbo].[tbl_WebSite]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WebSitePage'
ALTER TABLE [dbo].[tbl_WebSitePage]
ADD CONSTRAINT [PK_tbl_WebSitePage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSitePage_tbl_WebSite'
CREATE INDEX [IX_FK_tbl_WebSitePage_tbl_WebSite]
ON [dbo].[tbl_WebSitePage]
    ([WebSiteID]);