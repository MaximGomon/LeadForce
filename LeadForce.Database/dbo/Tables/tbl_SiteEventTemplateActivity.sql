-- Creating table 'tbl_SiteEventTemplateActivity'
CREATE TABLE [dbo].[tbl_SiteEventTemplateActivity] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [EventCategoryID] int  NULL,
    [ActivityTypeID] int  NULL,
    [ActivityCode] nvarchar(255)  NULL,
    [ActualPeriod] int  NULL,
    [Option] nvarchar(255)  NULL,
    [FormulaID] int  NULL,
    [Value] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [ActivityTypeID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes]
    FOREIGN KEY ([ActivityTypeID])
    REFERENCES [dbo].[tbl_ActivityTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [EventCategoryID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_EventCategories]
    FOREIGN KEY ([EventCategoryID])
    REFERENCES [dbo].[tbl_EventCategories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [FormulaID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_Formula]
    FOREIGN KEY ([FormulaID])
    REFERENCES [dbo].[tbl_Formula]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [PK_tbl_SiteEventTemplateActivity]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([ActivityTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_EventCategories'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_EventCategories]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([EventCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_Formula'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_Formula]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([FormulaID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([SiteEventTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_Sites]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([SiteID]);