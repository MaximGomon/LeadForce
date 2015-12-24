-- Creating table 'tbl_SiteEventActionTemplate'
CREATE TABLE [dbo].[tbl_SiteEventActionTemplate] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [SiteActionTemplateID] uniqueidentifier  NOT NULL,
    [StartAfter] int  NOT NULL,
    [StartAfterTypeID] int  NOT NULL,
    [MessageText] nvarchar(2000)  NULL
);
GO
-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [StartAfterTypeID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes]
    FOREIGN KEY ([StartAfterTypeID])
    REFERENCES [dbo].[tbl_StartAfterTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [PK_tbl_SiteEventActionTemplate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([SiteActionTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([SiteEventTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_Sites]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([StartAfterTypeID]);