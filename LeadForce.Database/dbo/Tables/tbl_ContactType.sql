-- Creating table 'tbl_ContactType'
CREATE TABLE [dbo].[tbl_ContactType] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactType'
ALTER TABLE [dbo].[tbl_ContactType]
ADD CONSTRAINT [FK_tbl_ContactType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactType'
ALTER TABLE [dbo].[tbl_ContactType]
ADD CONSTRAINT [PK_tbl_ContactType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactType_tbl_Sites]
ON [dbo].[tbl_ContactType]
    ([SiteID]);