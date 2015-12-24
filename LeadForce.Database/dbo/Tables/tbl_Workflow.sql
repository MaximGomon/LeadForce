-- Creating table 'tbl_Workflow'
CREATE TABLE [dbo].[tbl_Workflow] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Author] uniqueidentifier  NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NULL,
    [Status] int  NOT NULL,
    [MassWorkflowID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [MassWorkflowID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [FK_tbl_Workflow_tbl_MassWorkflow]
    FOREIGN KEY ([MassWorkflowID])
    REFERENCES [dbo].[tbl_MassWorkflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [FK_tbl_Workflow_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [FK_tbl_Workflow_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [PK_tbl_Workflow]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Workflow_tbl_MassWorkflow'
CREATE INDEX [IX_FK_tbl_Workflow_tbl_MassWorkflow]
ON [dbo].[tbl_Workflow]
    ([MassWorkflowID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Workflow_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Workflow_tbl_Sites]
ON [dbo].[tbl_Workflow]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Workflow_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_Workflow_tbl_WorkflowTemplate]
ON [dbo].[tbl_Workflow]
    ([WorkflowTemplateID]);