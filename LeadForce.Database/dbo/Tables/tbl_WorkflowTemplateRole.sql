-- Creating table 'tbl_WorkflowTemplateRole'
CREATE TABLE [dbo].[tbl_WorkflowTemplateRole] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ResponsibleID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateRole'
ALTER TABLE [dbo].[tbl_WorkflowTemplateRole]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateRole'
ALTER TABLE [dbo].[tbl_WorkflowTemplateRole]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateRole]
    ([WorkflowTemplateID]);