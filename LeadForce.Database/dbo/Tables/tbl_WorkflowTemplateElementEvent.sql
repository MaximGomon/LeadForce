-- Creating table 'tbl_WorkflowTemplateElementEvent'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementEvent] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Condition] int  NOT NULL,
    [ActivityCount] int  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementEvent]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementEvent]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementEvent]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementEvent]
    ([WorkflowTemplateElementID]);