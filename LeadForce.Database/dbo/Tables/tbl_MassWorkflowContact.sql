-- Creating table 'tbl_MassWorkflowContact'
CREATE TABLE [dbo].[tbl_MassWorkflowContact] (
    [ID] uniqueidentifier  NOT NULL,
    [MassWorkflowID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [MassWorkflowID] in table 'tbl_MassWorkflowContact'
ALTER TABLE [dbo].[tbl_MassWorkflowContact]
ADD CONSTRAINT [FK_tbl_MassWorkflowContact_tbl_MassWorkflow]
    FOREIGN KEY ([MassWorkflowID])
    REFERENCES [dbo].[tbl_MassWorkflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_MassWorkflowContact'
ALTER TABLE [dbo].[tbl_MassWorkflowContact]
ADD CONSTRAINT [PK_tbl_MassWorkflowContact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassWorkflowContact_tbl_MassWorkflow'
CREATE INDEX [IX_FK_tbl_MassWorkflowContact_tbl_MassWorkflow]
ON [dbo].[tbl_MassWorkflowContact]
    ([MassWorkflowID]);