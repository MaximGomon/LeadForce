-- Creating table 'tbl_ContactFunctionInCompany'
CREATE TABLE [dbo].[tbl_ContactFunctionInCompany] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactFunctionInCompany'
ALTER TABLE [dbo].[tbl_ContactFunctionInCompany]
ADD CONSTRAINT [FK_tbl_ContactFunctionInCompany_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactFunctionInCompany'
ALTER TABLE [dbo].[tbl_ContactFunctionInCompany]
ADD CONSTRAINT [PK_tbl_ContactFunctionInCompany]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactFunctionInCompany_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactFunctionInCompany_tbl_Sites]
ON [dbo].[tbl_ContactFunctionInCompany]
    ([SiteID]);