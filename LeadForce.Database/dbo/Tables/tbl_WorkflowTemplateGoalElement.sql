-- Creating table 'tbl_WorkflowTemplateGoalElement'
CREATE TABLE [dbo].[tbl_WorkflowTemplateGoalElement] (
    [tbl_WorkflowTemplateElement_ID] uniqueidentifier  NOT NULL,
    [tbl_WorkflowTemplateGoal_ID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [tbl_WorkflowTemplateElement_ID] in table 'tbl_WorkflowTemplateGoalElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([tbl_WorkflowTemplateElement_ID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [tbl_WorkflowTemplateGoal_ID] in table 'tbl_WorkflowTemplateGoalElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal]
    FOREIGN KEY ([tbl_WorkflowTemplateGoal_ID])
    REFERENCES [dbo].[tbl_WorkflowTemplateGoal]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [tbl_WorkflowTemplateElement_ID], [tbl_WorkflowTemplateGoal_ID] in table 'tbl_WorkflowTemplateGoalElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateGoalElement]
    PRIMARY KEY CLUSTERED ([tbl_WorkflowTemplateElement_ID], [tbl_WorkflowTemplateGoal_ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal]
ON [dbo].[tbl_WorkflowTemplateGoalElement]
    ([tbl_WorkflowTemplateGoal_ID]);