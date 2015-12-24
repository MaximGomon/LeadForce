-- Creating table 'tbl_ProductCategory'
CREATE TABLE [dbo].[tbl_ProductCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_ProductCategory'
ALTER TABLE [dbo].[tbl_ProductCategory]
ADD CONSTRAINT [FK_tbl_ProductCategory_tbl_ProductCategory]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ParentID] in table 'tbl_ProductCategory'
ALTER TABLE [dbo].[tbl_ProductCategory]
ADD CONSTRAINT [FK_tbl_ProductCategory_tbl_ProductCategory1]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_ProductCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ProductCategory'
ALTER TABLE [dbo].[tbl_ProductCategory]
ADD CONSTRAINT [PK_tbl_ProductCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductCategory_tbl_ProductCategory'
CREATE INDEX [IX_FK_tbl_ProductCategory_tbl_ProductCategory]
ON [dbo].[tbl_ProductCategory]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductCategory_tbl_ProductCategory1'
CREATE INDEX [IX_FK_tbl_ProductCategory_tbl_ProductCategory1]
ON [dbo].[tbl_ProductCategory]
    ([ParentID]);