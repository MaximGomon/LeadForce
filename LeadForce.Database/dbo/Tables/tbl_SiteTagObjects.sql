-- Creating table 'tbl_SiteTagObjects'
CREATE TABLE [dbo].[tbl_SiteTagObjects] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteTagID] uniqueidentifier  NOT NULL,
    [ObjectID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteTagID] in table 'tbl_SiteTagObjects'
ALTER TABLE [dbo].[tbl_SiteTagObjects]
ADD CONSTRAINT [FK_tbl_SiteTagObjects_tbl_SiteTags]
    FOREIGN KEY ([SiteTagID])
    REFERENCES [dbo].[tbl_SiteTags]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteTagObjects'
ALTER TABLE [dbo].[tbl_SiteTagObjects]
ADD CONSTRAINT [PK_tbl_SiteTagObjects]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteTagObjects_tbl_SiteTags'
CREATE INDEX [IX_FK_tbl_SiteTagObjects_tbl_SiteTags]
ON [dbo].[tbl_SiteTagObjects]
    ([SiteTagID]);