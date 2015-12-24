-- Creating table 'tbl_TaskHistory'
CREATE TABLE [dbo].[tbl_TaskHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [DateStart] datetime  NULL,
    [DateEnd] datetime  NULL,
    [DateOfControl] datetime  NULL,
    [IsImportantTask] bit  NOT NULL,
    [PlanDurationMinutes] int  NULL,
    [PlanDurationHours] int  NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [TaskStatusID] int  NOT NULL,
    [TaskResultID] uniqueidentifier  NULL,
    [DetailedResult] nvarchar(2000)  NULL
);
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [FK_tbl_TaskHistory_tbl_Contact]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [FK_tbl_TaskHistory_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskResultID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [FK_tbl_TaskHistory_tbl_TaskResult]
    FOREIGN KEY ([TaskResultID])
    REFERENCES [dbo].[tbl_TaskResult]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [PK_tbl_TaskHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_TaskHistory_tbl_Contact]
ON [dbo].[tbl_TaskHistory]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskHistory_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskHistory_tbl_Task]
ON [dbo].[tbl_TaskHistory]
    ([TaskID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskHistory_tbl_TaskResult'
CREATE INDEX [IX_FK_tbl_TaskHistory_tbl_TaskResult]
ON [dbo].[tbl_TaskHistory]
    ([TaskResultID]);