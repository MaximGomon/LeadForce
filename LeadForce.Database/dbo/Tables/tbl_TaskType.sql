-- Creating table 'tbl_TaskType'
CREATE TABLE [dbo].[tbl_TaskType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [StandardDurationHours] int  NOT NULL,
    [StandardDurationMinutes] int  NOT NULL,
    [TaskTypeCategoryID] int  NOT NULL,
    [TaskTypeAdjustDurationID] int  NOT NULL,
    [IsPublicEvent] bit  NOT NULL,
    [TaskTypePaymentSchemeID] int  NOT NULL,
    [IsTimePayment] bit  NOT NULL,
    [ProductID] uniqueidentifier  NULL,
    [TaskMembersCountID] int  NULL
);
GO
-- Creating foreign key on [ProductID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskMembersCountID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskMembersCount]
    FOREIGN KEY ([TaskMembersCountID])
    REFERENCES [dbo].[tbl_TaskMembersCount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskTypeAdjustDurationID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypeAdjustDuration]
    FOREIGN KEY ([TaskTypeAdjustDurationID])
    REFERENCES [dbo].[tbl_TaskTypeAdjustDuration]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskTypeCategoryID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypeCategory]
    FOREIGN KEY ([TaskTypeCategoryID])
    REFERENCES [dbo].[tbl_TaskTypeCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskTypePaymentSchemeID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypePaymentScheme]
    FOREIGN KEY ([TaskTypePaymentSchemeID])
    REFERENCES [dbo].[tbl_TaskTypePaymentScheme]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [PK_tbl_TaskType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_Product'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_Product]
ON [dbo].[tbl_TaskType]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_Sites]
ON [dbo].[tbl_TaskType]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskMembersCount'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskMembersCount]
ON [dbo].[tbl_TaskType]
    ([TaskMembersCountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskTypeAdjustDuration'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskTypeAdjustDuration]
ON [dbo].[tbl_TaskType]
    ([TaskTypeAdjustDurationID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskTypeCategory'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskTypeCategory]
ON [dbo].[tbl_TaskType]
    ([TaskTypeCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskTypePaymentScheme'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskTypePaymentScheme]
ON [dbo].[tbl_TaskType]
    ([TaskTypePaymentSchemeID]);