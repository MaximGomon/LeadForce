-- Creating table 'tbl_WorkflowElement'
CREATE TABLE [dbo].[tbl_WorkflowElement] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [ControlDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Result] nvarchar(255)  NULL,
    [Status] int  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [WorkflowID] in table 'tbl_WorkflowElement'
ALTER TABLE [dbo].[tbl_WorkflowElement]
ADD CONSTRAINT [FK_tbl_WorkflowElement_tbl_Workflow]
    FOREIGN KEY ([WorkflowID])
    REFERENCES [dbo].[tbl_Workflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowElement'
ALTER TABLE [dbo].[tbl_WorkflowElement]
ADD CONSTRAINT [FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowElement'
ALTER TABLE [dbo].[tbl_WorkflowElement]
ADD CONSTRAINT [PK_tbl_WorkflowElement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowElement_tbl_Workflow'
CREATE INDEX [IX_FK_tbl_WorkflowElement_tbl_Workflow]
ON [dbo].[tbl_WorkflowElement]
    ([WorkflowID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowElement]
    ([WorkflowTemplateElementID]);