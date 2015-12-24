-- Creating table 'tbl_Links'
CREATE TABLE [dbo].[tbl_Links] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [RuleTypeID] int  NOT NULL,
    [Code] nvarchar(50)  NULL,
    [URL] nvarchar(255)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [FileSize] bigint  NULL,
    [Description] nvarchar(500)  NULL
);
GO
-- Creating foreign key on [RuleTypeID] in table 'tbl_Links'
ALTER TABLE [dbo].[tbl_Links]
ADD CONSTRAINT [FK_tbl_Links_tbl_RuleTypes]
    FOREIGN KEY ([RuleTypeID])
    REFERENCES [dbo].[tbl_RuleTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Links'
ALTER TABLE [dbo].[tbl_Links]
ADD CONSTRAINT [FK_tbl_Links_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Links'
ALTER TABLE [dbo].[tbl_Links]
ADD CONSTRAINT [PK_tbl_Links]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Links_tbl_RuleTypes'
CREATE INDEX [IX_FK_tbl_Links_tbl_RuleTypes]
ON [dbo].[tbl_Links]
    ([RuleTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Links_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Links_tbl_Sites]
ON [dbo].[tbl_Links]
    ([SiteID]);