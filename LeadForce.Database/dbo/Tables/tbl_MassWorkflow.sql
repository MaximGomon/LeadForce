-- Creating table 'tbl_MassWorkflow'
CREATE TABLE [dbo].[tbl_MassWorkflow] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Status] int  NOT NULL,
    [StartDate] datetime  NULL,
    [MassWorkflowTypeID] int  NULL
);
GO
-- Creating foreign key on [MassWorkflowTypeID] in table 'tbl_MassWorkflow'
ALTER TABLE [dbo].[tbl_MassWorkflow]
ADD CONSTRAINT [FK_tbl_MassWorkflow_tbl_MassWorkflowType]
    FOREIGN KEY ([MassWorkflowTypeID])
    REFERENCES [dbo].[tbl_MassWorkflowType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_MassWorkflow'
ALTER TABLE [dbo].[tbl_MassWorkflow]
ADD CONSTRAINT [FK_tbl_MassWorkflow_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_MassWorkflow'
ALTER TABLE [dbo].[tbl_MassWorkflow]
ADD CONSTRAINT [PK_tbl_MassWorkflow]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassWorkflow_tbl_MassWorkflowType'
CREATE INDEX [IX_FK_tbl_MassWorkflow_tbl_MassWorkflowType]
ON [dbo].[tbl_MassWorkflow]
    ([MassWorkflowTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassWorkflow_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MassWorkflow_tbl_Sites]
ON [dbo].[tbl_MassWorkflow]
    ([SiteID]);