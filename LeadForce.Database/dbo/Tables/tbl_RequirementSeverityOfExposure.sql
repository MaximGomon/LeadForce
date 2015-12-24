-- Creating table 'tbl_RequirementSeverityOfExposure'
CREATE TABLE [dbo].[tbl_RequirementSeverityOfExposure] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [RequirementTypeID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ParentID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_RequirementSeverityOfExposure]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementTypeID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType]
    FOREIGN KEY ([RequirementTypeID])
    REFERENCES [dbo].[tbl_RequirementType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [PK_tbl_RequirementSeverityOfExposure]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure'
CREATE INDEX [IX_FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure]
ON [dbo].[tbl_RequirementSeverityOfExposure]
    ([ParentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType'
CREATE INDEX [IX_FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType]
ON [dbo].[tbl_RequirementSeverityOfExposure]
    ([RequirementTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSeverityOfExposure_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementSeverityOfExposure_tbl_Sites]
ON [dbo].[tbl_RequirementSeverityOfExposure]
    ([SiteID]);