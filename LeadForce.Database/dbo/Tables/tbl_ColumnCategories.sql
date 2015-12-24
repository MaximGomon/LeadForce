-- Creating table 'tbl_ColumnCategories'
CREATE TABLE [dbo].[tbl_ColumnCategories] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_ColumnCategories'
ALTER TABLE [dbo].[tbl_ColumnCategories]
ADD CONSTRAINT [FK_tbl_ColumnCategories_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ColumnCategories'
ALTER TABLE [dbo].[tbl_ColumnCategories]
ADD CONSTRAINT [PK_tbl_ColumnCategories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ColumnCategories_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ColumnCategories_tbl_Sites]
ON [dbo].[tbl_ColumnCategories]
    ([SiteID]);