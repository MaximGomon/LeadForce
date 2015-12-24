-- Creating table 'tbl_RequirementComplexity'
CREATE TABLE [dbo].[tbl_RequirementComplexity] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_RequirementComplexity'
ALTER TABLE [dbo].[tbl_RequirementComplexity]
ADD CONSTRAINT [FK_tbl_RequirementComplexity_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementComplexity'
ALTER TABLE [dbo].[tbl_RequirementComplexity]
ADD CONSTRAINT [PK_tbl_RequirementComplexity]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComplexity_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementComplexity_tbl_Sites]
ON [dbo].[tbl_RequirementComplexity]
    ([SiteID]);