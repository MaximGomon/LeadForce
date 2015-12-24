-- Creating table 'tbl_SourceMonitoring'
CREATE TABLE [dbo].[tbl_SourceMonitoring] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [SourceTypeID] int  NOT NULL,
    [StatusID] int  NOT NULL,
    [Comment] nvarchar(2000)  NULL,
    [PopHost] nvarchar(255)  NOT NULL,
    [PopUserName] nvarchar(255)  NOT NULL,
    [PopPassword] nvarchar(255)  NOT NULL,
    [PopPort] int  NOT NULL,
    [IsSsl] bit  NOT NULL,
    [IsLeaveOnServer] bit  NOT NULL,
    [LastReceivedAt] datetime  NULL,
    [DaysToDelete] int  NULL,
    [SenderProcessingID] int  NOT NULL,
    [ProcessingOfReturnsID] int  NOT NULL,
    [IsRemoveReturns] bit  NOT NULL,
    [ProcessingOfAutoRepliesID] int  NOT NULL,
    [IsRemoveAutoReplies] bit  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL,
    [ReceiverContactID] uniqueidentifier  NULL,
    [StartDate] datetime  NULL
);
GO
-- Creating foreign key on [ReceiverContactID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [FK_tbl_SourceMonitoring_tbl_ReceiverContact]
    FOREIGN KEY ([ReceiverContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [FK_tbl_SourceMonitoring_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [FK_tbl_SourceMonitoring_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [PK_tbl_SourceMonitoring]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoring_tbl_ReceiverContact'
CREATE INDEX [IX_FK_tbl_SourceMonitoring_tbl_ReceiverContact]
ON [dbo].[tbl_SourceMonitoring]
    ([ReceiverContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoring_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_SourceMonitoring_tbl_RequestSourceType]
ON [dbo].[tbl_SourceMonitoring]
    ([RequestSourceTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoring_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SourceMonitoring_tbl_Sites]
ON [dbo].[tbl_SourceMonitoring]
    ([SiteID]);