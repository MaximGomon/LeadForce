-- Creating table 'tbl_SocialAuthorizationToken'
CREATE TABLE [dbo].[tbl_SocialAuthorizationToken] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [PortalSettingsID] uniqueidentifier  NOT NULL,
    [ExpirationDate] datetime  NOT NULL
);
GO
-- Creating foreign key on [PortalSettingsID] in table 'tbl_SocialAuthorizationToken'
ALTER TABLE [dbo].[tbl_SocialAuthorizationToken]
ADD CONSTRAINT [FK_tbl_SocialAuthorizationToken_tbl_PortalSettings]
    FOREIGN KEY ([PortalSettingsID])
    REFERENCES [dbo].[tbl_PortalSettings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_SocialAuthorizationToken'
ALTER TABLE [dbo].[tbl_SocialAuthorizationToken]
ADD CONSTRAINT [FK_tbl_SocialAuthorizationToken_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SocialAuthorizationToken'
ALTER TABLE [dbo].[tbl_SocialAuthorizationToken]
ADD CONSTRAINT [PK_tbl_SocialAuthorizationToken]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SocialAuthorizationToken_tbl_PortalSettings'
CREATE INDEX [IX_FK_tbl_SocialAuthorizationToken_tbl_PortalSettings]
ON [dbo].[tbl_SocialAuthorizationToken]
    ([PortalSettingsID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SocialAuthorizationToken_tbl_User'
CREATE INDEX [IX_FK_tbl_SocialAuthorizationToken_tbl_User]
ON [dbo].[tbl_SocialAuthorizationToken]
    ([UserID]);