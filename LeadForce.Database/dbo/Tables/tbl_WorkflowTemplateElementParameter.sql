-- Creating table 'tbl_WorkflowTemplateElementParameter'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementParameter] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementParameter]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementParameter]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementParameter]
    ([WorkflowTemplateElementID]);