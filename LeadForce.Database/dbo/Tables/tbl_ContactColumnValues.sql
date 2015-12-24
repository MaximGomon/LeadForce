-- Creating table 'tbl_ContactColumnValues'
CREATE TABLE [dbo].[tbl_ContactColumnValues] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL,
    [StringValue] nvarchar(512)  NULL,
    [DateValue] datetime  NULL,
    [SiteColumnValueID] uniqueidentifier  NULL,
    [LogicalValue] bit  NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [FK_tbl_ContactColumnValues_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteColumnID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [FK_tbl_ContactColumnValues_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteColumnValueID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [FK_tbl_ContactColumnValues_tbl_SiteColumnValues]
    FOREIGN KEY ([SiteColumnValueID])
    REFERENCES [dbo].[tbl_SiteColumnValues]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [PK_tbl_ContactColumnValues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactColumnValues_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactColumnValues_tbl_Contact]
ON [dbo].[tbl_ContactColumnValues]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactColumnValues_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_ContactColumnValues_tbl_SiteColumns]
ON [dbo].[tbl_ContactColumnValues]
    ([SiteColumnID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactColumnValues_tbl_SiteColumnValues'
CREATE INDEX [IX_FK_tbl_ContactColumnValues_tbl_SiteColumnValues]
ON [dbo].[tbl_ContactColumnValues]
    ([SiteColumnValueID]);