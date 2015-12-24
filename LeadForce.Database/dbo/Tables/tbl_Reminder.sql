-- Creating table 'tbl_Reminder'
CREATE TABLE [dbo].[tbl_Reminder] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(2000)  NOT NULL,
    [ReminderDate] datetime  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [ObjectID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_Reminder'
ALTER TABLE [dbo].[tbl_Reminder]
ADD CONSTRAINT [FK_tbl_Reminder_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleID] in table 'tbl_Reminder'
ALTER TABLE [dbo].[tbl_Reminder]
ADD CONSTRAINT [FK_tbl_Reminder_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Reminder'
ALTER TABLE [dbo].[tbl_Reminder]
ADD CONSTRAINT [PK_tbl_Reminder]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Reminder_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Reminder_tbl_Contact]
ON [dbo].[tbl_Reminder]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Reminder_tbl_Module'
CREATE INDEX [IX_FK_tbl_Reminder_tbl_Module]
ON [dbo].[tbl_Reminder]
    ([ModuleID]);