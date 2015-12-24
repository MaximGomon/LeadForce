-- Creating table 'tbl_WorkflowTemplateElementRelation'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementRelation] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [StartElementID] uniqueidentifier  NOT NULL,
    [StartElementResult] nvarchar(255)  NULL,
    [EndElementID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateElementRelation'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementRelation]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementRelation'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementRelation]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementRelation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateElementRelation]
    ([WorkflowTemplateID]);