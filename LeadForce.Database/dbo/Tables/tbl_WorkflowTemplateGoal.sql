-- Creating table 'tbl_WorkflowTemplateGoal'
CREATE TABLE [dbo].[tbl_WorkflowTemplateGoal] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Description] nvarchar(2048)  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateGoal'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoal]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateGoal'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoal]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateGoal]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateGoal]
    ([WorkflowTemplateID]);