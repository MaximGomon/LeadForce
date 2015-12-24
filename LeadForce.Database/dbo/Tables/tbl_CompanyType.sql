-- Creating table 'tbl_CompanyType'
CREATE TABLE [dbo].[tbl_CompanyType] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_CompanyType'
ALTER TABLE [dbo].[tbl_CompanyType]
ADD CONSTRAINT [FK_tbl_CompanyType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_CompanyType'
ALTER TABLE [dbo].[tbl_CompanyType]
ADD CONSTRAINT [PK_tbl_CompanyType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanyType_tbl_Sites]
ON [dbo].[tbl_CompanyType]
    ([SiteID]);