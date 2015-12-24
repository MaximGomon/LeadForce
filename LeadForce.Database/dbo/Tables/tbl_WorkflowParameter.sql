-- Creating table 'tbl_WorkflowParameter'
CREATE TABLE [dbo].[tbl_WorkflowParameter] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateParameterID] uniqueidentifier  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [WorkflowID] in table 'tbl_WorkflowParameter'
ALTER TABLE [dbo].[tbl_WorkflowParameter]
ADD CONSTRAINT [FK_tbl_WorkflowParameter_tbl_Workflow]
    FOREIGN KEY ([WorkflowID])
    REFERENCES [dbo].[tbl_Workflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [WorkflowTemplateParameterID] in table 'tbl_WorkflowParameter'
ALTER TABLE [dbo].[tbl_WorkflowParameter]
ADD CONSTRAINT [FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter]
    FOREIGN KEY ([WorkflowTemplateParameterID])
    REFERENCES [dbo].[tbl_WorkflowTemplateParameter]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowParameter'
ALTER TABLE [dbo].[tbl_WorkflowParameter]
ADD CONSTRAINT [PK_tbl_WorkflowParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowParameter_tbl_Workflow'
CREATE INDEX [IX_FK_tbl_WorkflowParameter_tbl_Workflow]
ON [dbo].[tbl_WorkflowParameter]
    ([WorkflowID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter'
CREATE INDEX [IX_FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter]
ON [dbo].[tbl_WorkflowParameter]
    ([WorkflowTemplateParameterID]);