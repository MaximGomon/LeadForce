-- Creating table 'tbl_SiteEventTemplates'
CREATE TABLE [dbo].[tbl_SiteEventTemplates] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [LogicConditionID] int  NOT NULL,
    [ActionCount] int  NULL,
    [EventConditions] nvarchar(max)  NULL,
    [FrequencyPeriod] int  NOT NULL,
    [OwnerID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [LogicConditionID] in table 'tbl_SiteEventTemplates'
ALTER TABLE [dbo].[tbl_SiteEventTemplates]
ADD CONSTRAINT [FK_tbl_SiteEventTemplates_tbl_LogicConditions]
    FOREIGN KEY ([LogicConditionID])
    REFERENCES [dbo].[tbl_LogicConditions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteEventTemplates'
ALTER TABLE [dbo].[tbl_SiteEventTemplates]
ADD CONSTRAINT [FK_tbl_SiteEventTemplates_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteEventTemplates'
ALTER TABLE [dbo].[tbl_SiteEventTemplates]
ADD CONSTRAINT [PK_tbl_SiteEventTemplates]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplates_tbl_LogicConditions'
CREATE INDEX [IX_FK_tbl_SiteEventTemplates_tbl_LogicConditions]
ON [dbo].[tbl_SiteEventTemplates]
    ([LogicConditionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplates_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventTemplates_tbl_Sites]
ON [dbo].[tbl_SiteEventTemplates]
    ([SiteID]);