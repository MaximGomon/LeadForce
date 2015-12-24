-- Creating table 'tbl_WorkflowTemplateElementExternalRequest'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementExternalRequest'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementExternalRequest'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementExternalRequest]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementExternalRequest]
    ([WorkflowTemplateElementID]);