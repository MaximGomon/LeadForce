-- Creating table 'tbl_SiteColumns'
CREATE TABLE [dbo].[tbl_SiteColumns] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NULL,
    [Name] nvarchar(255)  NOT NULL,
    [CategoryID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Code] nvarchar(255)  NOT NULL
);
GO
-- Creating foreign key on [CategoryID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [FK_tbl_SiteColumns_tbl_ColumnCategories]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[tbl_ColumnCategories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TypeID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [FK_tbl_SiteColumns_tbl_ColumnTypes]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[tbl_ColumnTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [FK_tbl_SiteColumns_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [PK_tbl_SiteColumns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumns_tbl_ColumnCategories'
CREATE INDEX [IX_FK_tbl_SiteColumns_tbl_ColumnCategories]
ON [dbo].[tbl_SiteColumns]
    ([CategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumns_tbl_ColumnTypes'
CREATE INDEX [IX_FK_tbl_SiteColumns_tbl_ColumnTypes]
ON [dbo].[tbl_SiteColumns]
    ([TypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumns_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteColumns_tbl_Sites]
ON [dbo].[tbl_SiteColumns]
    ([SiteID]);