-- Creating table 'tbl_ContactActivity'
CREATE TABLE [dbo].[tbl_ContactActivity] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ActivityTypeID] int  NOT NULL,
    [ActivityCode] nvarchar(255)  NULL,
    [ContactSessionID] uniqueidentifier  NULL,
    [SourceMonitoringID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ActivityTypeID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_ActivityTypes]
    FOREIGN KEY ([ActivityTypeID])
    REFERENCES [dbo].[tbl_ActivityTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactSessionID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_ContactSessions]
    FOREIGN KEY ([ContactSessionID])
    REFERENCES [dbo].[tbl_ContactSessions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SourceMonitoringID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [PK_tbl_ContactActivity]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_ActivityTypes'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_ActivityTypes]
ON [dbo].[tbl_ContactActivity]
    ([ActivityTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_Contact]
ON [dbo].[tbl_ContactActivity]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_ContactSessions'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_ContactSessions]
ON [dbo].[tbl_ContactActivity]
    ([ContactSessionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_Sites]
ON [dbo].[tbl_ContactActivity]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_SourceMonitoring]
ON [dbo].[tbl_ContactActivity]
    ([SourceMonitoringID]);