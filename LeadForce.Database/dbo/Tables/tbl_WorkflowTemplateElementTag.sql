-- Creating table 'tbl_WorkflowTemplateElementTag'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementTag] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [SiteTagID] uniqueidentifier  NOT NULL,
    [Operation] int  NOT NULL
);
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementTag'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementTag]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementTag'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementTag]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementTag]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementTag]
    ([WorkflowTemplateElementID]);