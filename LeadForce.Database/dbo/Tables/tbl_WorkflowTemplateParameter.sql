-- Creating table 'tbl_WorkflowTemplateParameter'
CREATE TABLE [dbo].[tbl_WorkflowTemplateParameter] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NULL,
    [ModuleID] uniqueidentifier  NULL,
    [IsSystem] bit  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateParameter]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateParameter]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateParameter]
    ([WorkflowTemplateID]);