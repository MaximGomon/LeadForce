-- Creating table 'tbl_WorkflowTemplateElementPeriod'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementPeriod] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [DayWeek] int  NOT NULL,
    [FromTime] time  NULL,
    [ToTime] time  NULL
);
GO
-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementPeriod'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementPeriod]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementPeriod'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementPeriod]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementPeriod]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementPeriod]
    ([WorkflowTemplateElementID]);