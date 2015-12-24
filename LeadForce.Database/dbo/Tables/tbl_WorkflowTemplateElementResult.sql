-- Creating table 'tbl_WorkflowTemplateElementResult'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementResult] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsSystem] bit  NOT NULL
);
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementResult'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementResult]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementResult'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementResult]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementResult]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementResult]
    ([WorkflowTemplateElementID]);