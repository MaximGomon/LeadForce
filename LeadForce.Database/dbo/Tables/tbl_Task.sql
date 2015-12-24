-- Creating table 'tbl_Task'
CREATE TABLE [dbo].[tbl_Task] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(2000)  NOT NULL,
    [TaskTypeID] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [DateOfControl] datetime  NOT NULL,
    [IsImportantTask] bit  NOT NULL,
    [PlanDurationMinutes] int  NOT NULL,
    [PlanDurationHours] int  NOT NULL,
    [ResponsibleID] uniqueidentifier  NOT NULL,
    [CreatorID] uniqueidentifier  NOT NULL,
    [ResponsibleReminderDate] datetime  NULL,
    [CreatorReminderDate] datetime  NULL,
    [IsUrgentTask] bit  NOT NULL,
    [TaskStatusID] int  NOT NULL,
    [TaskResultID] uniqueidentifier  NULL,
    [DetailedResult] nvarchar(2000)  NULL,
    [ActualDurationMinutes] int  NULL,
    [ActualDurationHours] int  NULL,
    [CompletePercentage] decimal(18,4)  NULL,
    [OrderID] uniqueidentifier  NULL,
    [ProductID] uniqueidentifier  NULL,
    [MainMemberContactID] uniqueidentifier  NULL,
    [MainMemberCompanyID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [MainMemberCompanyID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_CompanyMainMember]
    FOREIGN KEY ([MainMemberCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CreatorID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_ContactCreator]
    FOREIGN KEY ([CreatorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [MainMemberContactID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_ContactMainMember]
    FOREIGN KEY ([MainMemberContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_ContactResponsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskResultID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_TaskResult]
    FOREIGN KEY ([TaskResultID])
    REFERENCES [dbo].[tbl_TaskResult]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskTypeID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_TaskType]
    FOREIGN KEY ([TaskTypeID])
    REFERENCES [dbo].[tbl_TaskType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [PK_tbl_Task]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_CompanyMainMember'
CREATE INDEX [IX_FK_tbl_Task_tbl_CompanyMainMember]
ON [dbo].[tbl_Task]
    ([MainMemberCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_ContactCreator'
CREATE INDEX [IX_FK_tbl_Task_tbl_ContactCreator]
ON [dbo].[tbl_Task]
    ([CreatorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_ContactMainMember'
CREATE INDEX [IX_FK_tbl_Task_tbl_ContactMainMember]
ON [dbo].[tbl_Task]
    ([MainMemberContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_ContactResponsible'
CREATE INDEX [IX_FK_tbl_Task_tbl_ContactResponsible]
ON [dbo].[tbl_Task]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_Order'
CREATE INDEX [IX_FK_tbl_Task_tbl_Order]
ON [dbo].[tbl_Task]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_Product'
CREATE INDEX [IX_FK_tbl_Task_tbl_Product]
ON [dbo].[tbl_Task]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_TaskResult'
CREATE INDEX [IX_FK_tbl_Task_tbl_TaskResult]
ON [dbo].[tbl_Task]
    ([TaskResultID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_TaskType'
CREATE INDEX [IX_FK_tbl_Task_tbl_TaskType]
ON [dbo].[tbl_Task]
    ([TaskTypeID]);