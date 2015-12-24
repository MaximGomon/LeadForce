-- Creating table 'tbl_Term'
CREATE TABLE [dbo].[tbl_Term] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [PublicationID] uniqueidentifier  NULL,
    [Code] nvarchar(100)  NULL,
    [Description] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [PublicationID] in table 'tbl_Term'
ALTER TABLE [dbo].[tbl_Term]
ADD CONSTRAINT [FK_tbl_Term_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Term'
ALTER TABLE [dbo].[tbl_Term]
ADD CONSTRAINT [FK_tbl_Term_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Term'
ALTER TABLE [dbo].[tbl_Term]
ADD CONSTRAINT [PK_tbl_Term]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Term_tbl_Publication'
CREATE INDEX [IX_FK_tbl_Term_tbl_Publication]
ON [dbo].[tbl_Term]
    ([PublicationID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Term_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Term_tbl_Sites]
ON [dbo].[tbl_Term]
    ([SiteID]);