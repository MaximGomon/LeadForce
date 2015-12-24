-- Creating table 'tbl_CompanySize'
CREATE TABLE [dbo].[tbl_CompanySize] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_CompanySize'
ALTER TABLE [dbo].[tbl_CompanySize]
ADD CONSTRAINT [FK_tbl_CompanySize_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_CompanySize'
ALTER TABLE [dbo].[tbl_CompanySize]
ADD CONSTRAINT [PK_tbl_CompanySize]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanySize_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanySize_tbl_Sites]
ON [dbo].[tbl_CompanySize]
    ([SiteID]);