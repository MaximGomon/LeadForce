-- Creating table 'tbl_Sites'
CREATE TABLE [dbo].[tbl_Sites] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [SmtpHost] nvarchar(255)  NULL,
    [SmtpUsername] nvarchar(255)  NULL,
    [SmtpPassword] nvarchar(255)  NULL,
    [SmtpPort] int  NULL,
    [SystemEmail] nvarchar(255)  NULL,
    [IsAllowUseSystemEmail] bit  NOT NULL,
    [IsSendEmailToSubscribedUser] bit  NOT NULL,
    [IsUnsubscribe] bit  NOT NULL,
    [UnsubscribeActionID] int  NULL,
    [IsServiceAdvertising] bit  NOT NULL,
    [ServiceAdvertisingActionID] int  NULL,
    [MaxFileSize] int  NULL,
    [FileQuota] int  NULL,
    [SessionTimeout] int  NOT NULL,
    [AccessProfileID] uniqueidentifier  NULL,
    [IsSendFromLeadForce] bit  NOT NULL,
    [ShowHiddenMessages] bit  NOT NULL,
    [LinkProcessingURL] nvarchar(3000)  NULL,
    [HtmlEditorModeID] int  NOT NULL,
    [IsTemplate] bit  NOT NULL,
    [UserSessionTimeout] int  NOT NULL,
    [IsBlockAccessFromDomainsOutsideOfList] bit  NOT NULL,
    [MainUserID] uniqueidentifier  NULL,
    [ActiveUntilDate] datetime  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [PayerCompanyID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [AccessProfileID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PayerCompanyID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_Company]
    FOREIGN KEY ([PayerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ServiceAdvertisingActionID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_EmailActions]
    FOREIGN KEY ([ServiceAdvertisingActionID])
    REFERENCES [dbo].[tbl_EmailActions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UnsubscribeActionID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_EmailActions1]
    FOREIGN KEY ([UnsubscribeActionID])
    REFERENCES [dbo].[tbl_EmailActions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [MainUserID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_User]
    FOREIGN KEY ([MainUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [PK_tbl_Sites]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_Sites_tbl_AccessProfile]
ON [dbo].[tbl_Sites]
    ([AccessProfileID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_Company'
CREATE INDEX [IX_FK_tbl_Sites_tbl_Company]
ON [dbo].[tbl_Sites]
    ([PayerCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_EmailActions'
CREATE INDEX [IX_FK_tbl_Sites_tbl_EmailActions]
ON [dbo].[tbl_Sites]
    ([ServiceAdvertisingActionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_EmailActions1'
CREATE INDEX [IX_FK_tbl_Sites_tbl_EmailActions1]
ON [dbo].[tbl_Sites]
    ([UnsubscribeActionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Sites_tbl_PriceList]
ON [dbo].[tbl_Sites]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_User'
CREATE INDEX [IX_FK_tbl_Sites_tbl_User]
ON [dbo].[tbl_Sites]
    ([MainUserID]);