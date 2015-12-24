-- Creating table 'tbl_WorkflowTemplateConditionEvent'
CREATE TABLE [dbo].[tbl_WorkflowTemplateConditionEvent] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NULL,
    [WorkflowTemplateElementEventID] uniqueidentifier  NULL,
    [Category] int  NOT NULL,
    [ActivityType] int  NULL,
    [Code] nvarchar(max)  NULL,
    [ActualPeriod] int  NULL,
    [Requisite] nvarchar(2000)  NULL,
    [Formula] int  NULL,
    [Value] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateConditionEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [WorkflowTemplateElementEventID] in table 'tbl_WorkflowTemplateConditionEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent]
    FOREIGN KEY ([WorkflowTemplateElementEventID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElementEvent]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateConditionEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateConditionEvent]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateConditionEvent]
    ([WorkflowTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent]
ON [dbo].[tbl_WorkflowTemplateConditionEvent]
    ([WorkflowTemplateElementEventID]);