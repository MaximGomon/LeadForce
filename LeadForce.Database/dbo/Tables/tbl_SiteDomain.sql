-- Creating table 'tbl_SiteDomain'
CREATE TABLE [dbo].[tbl_SiteDomain] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Domain] nvarchar(256)  NOT NULL,
    [Aliases] nvarchar(max)  NOT NULL,
    [Note] nvarchar(2048)  NOT NULL,
    [StatusID] int  NOT NULL,
    [PageCountWithCounter] int  NOT NULL,
    [TotalPageCount] int  NOT NULL,
    [PageCountWithForms] int  NOT NULL,
    [PageCountWithOnClosingForms] int  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteDomain'
ALTER TABLE [dbo].[tbl_SiteDomain]
ADD CONSTRAINT [FK_tbl_SiteDomain_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteDomain'
ALTER TABLE [dbo].[tbl_SiteDomain]
ADD CONSTRAINT [PK_tbl_SiteDomain]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteDomain_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteDomain_tbl_Sites]
ON [dbo].[tbl_SiteDomain]
    ([SiteID]);