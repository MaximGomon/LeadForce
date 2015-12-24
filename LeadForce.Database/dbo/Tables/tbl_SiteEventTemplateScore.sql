-- Creating table 'tbl_SiteEventTemplateScore'
CREATE TABLE [dbo].[tbl_SiteEventTemplateScore] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [SiteActivityScoreTypeID] uniqueidentifier  NOT NULL,
    [OperationID] int  NOT NULL,
    [Score] int  NOT NULL
);
GO
-- Creating foreign key on [OperationID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_Operations]
    FOREIGN KEY ([OperationID])
    REFERENCES [dbo].[tbl_Operations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActivityScoreTypeID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType]
    FOREIGN KEY ([SiteActivityScoreTypeID])
    REFERENCES [dbo].[tbl_SiteActivityScoreType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [PK_tbl_SiteEventTemplateScore]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_Operations'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_Operations]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([OperationID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([SiteActivityScoreTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([SiteEventTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_Sites]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([SiteID]);