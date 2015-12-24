-- Creating table 'tbl_SiteAction'
CREATE TABLE [dbo].[tbl_SiteAction] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActionTemplateID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [ActionStatusID] int  NOT NULL,
    [ActionDate] datetime  NOT NULL,
    [ResponseDate] datetime  NULL,
    [Comments] nvarchar(max)  NULL,
    [ObjectID] uniqueidentifier  NULL,
    [MessageTypeID] int  NULL,
    [MessageTitle] nvarchar(255)  NULL,
    [SourceMonitoringID] uniqueidentifier  NULL,
    [DirectionID] int  NOT NULL,
    [SenderID] uniqueidentifier  NULL,
    [MessageText] nvarchar(max)  NULL,
    [IsHidden] bit  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [POPMessageID] nvarchar(150)  NULL
);
GO
-- Creating foreign key on [ActionStatusID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_ActionStatus]
    FOREIGN KEY ([ActionStatusID])
    REFERENCES [dbo].[tbl_ActionStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SenderID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_Contact_Sender]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SourceMonitoringID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [PK_tbl_SiteAction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_ActionStatus'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_ActionStatus]
ON [dbo].[tbl_SiteAction]
    ([ActionStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_Contact'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_Contact]
ON [dbo].[tbl_SiteAction]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_Contact_Sender'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_Contact_Sender]
ON [dbo].[tbl_SiteAction]
    ([SenderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteAction]
    ([SiteActionTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_Sites]
ON [dbo].[tbl_SiteAction]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_SourceMonitoring]
ON [dbo].[tbl_SiteAction]
    ([SourceMonitoringID]);