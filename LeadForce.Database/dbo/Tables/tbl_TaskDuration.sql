-- Creating table 'tbl_TaskDuration'
CREATE TABLE [dbo].[tbl_TaskDuration] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [SectionDateStart] datetime  NULL,
    [SectionDateEnd] datetime  NULL,
    [ActualDurationMinutes] int  NULL,
    [ActualDurationHours] int  NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [Comment] nvarchar(1024)  NULL
);
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_TaskDuration'
ALTER TABLE [dbo].[tbl_TaskDuration]
ADD CONSTRAINT [FK_tbl_TaskDuration_tbl_ContactResponsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskID] in table 'tbl_TaskDuration'
ALTER TABLE [dbo].[tbl_TaskDuration]
ADD CONSTRAINT [FK_tbl_TaskDuration_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_TaskDuration'
ALTER TABLE [dbo].[tbl_TaskDuration]
ADD CONSTRAINT [PK_tbl_TaskDuration]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskDuration_tbl_ContactResponsible'
CREATE INDEX [IX_FK_tbl_TaskDuration_tbl_ContactResponsible]
ON [dbo].[tbl_TaskDuration]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskDuration_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskDuration_tbl_Task]
ON [dbo].[tbl_TaskDuration]
    ([TaskID]);