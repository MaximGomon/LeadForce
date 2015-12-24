-- Creating table 'tbl_SiteColumnValues'
CREATE TABLE [dbo].[tbl_SiteColumnValues] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL,
    [Value] nvarchar(50)  NULL
);
GO
-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteColumnValues'
ALTER TABLE [dbo].[tbl_SiteColumnValues]
ADD CONSTRAINT [FK_tbl_SiteColumnValues_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteColumnValues'
ALTER TABLE [dbo].[tbl_SiteColumnValues]
ADD CONSTRAINT [PK_tbl_SiteColumnValues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumnValues_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteColumnValues_tbl_SiteColumns]
ON [dbo].[tbl_SiteColumnValues]
    ([SiteColumnID]);