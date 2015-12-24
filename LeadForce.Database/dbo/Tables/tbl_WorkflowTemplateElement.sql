-- Creating table 'tbl_WorkflowTemplateElement'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElement] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ElementType] int  NOT NULL,
    [Optional] bit  NOT NULL,
    [ResultName] nvarchar(255)  NULL,
    [AllowOptionalTransfer] bit  NOT NULL,
    [ShowCurrentUser] bit  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Order] int  NOT NULL,
    [StartAfter] int  NOT NULL,
    [StartPeriod] int  NOT NULL,
    [DurationHours] int  NULL,
    [DurationMinutes] int  NULL,
    [ControlAfter] int  NULL,
    [ControlPeriod] int  NULL,
    [ControlFromBeginProccess] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO
-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElement]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElement]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateElement]
    ([WorkflowTemplateID]);