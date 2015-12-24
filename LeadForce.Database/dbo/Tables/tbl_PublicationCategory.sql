-- Creating table 'tbl_PublicationCategory'
CREATE TABLE [dbo].[tbl_PublicationCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [InHelp] bit  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [ParentID] in table 'tbl_PublicationCategory'
ALTER TABLE [dbo].[tbl_PublicationCategory]
ADD CONSTRAINT [FK_tbl_PublicationCategory_tbl_PublicationCategory1]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_PublicationCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PublicationCategory'
ALTER TABLE [dbo].[tbl_PublicationCategory]
ADD CONSTRAINT [FK_tbl_PublicationCategory_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PublicationCategory'
ALTER TABLE [dbo].[tbl_PublicationCategory]
ADD CONSTRAINT [PK_tbl_PublicationCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationCategory_tbl_PublicationCategory1'
CREATE INDEX [IX_FK_tbl_PublicationCategory_tbl_PublicationCategory1]
ON [dbo].[tbl_PublicationCategory]
    ([ParentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationCategory_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PublicationCategory_tbl_Sites]
ON [dbo].[tbl_PublicationCategory]
    ([SiteID]);