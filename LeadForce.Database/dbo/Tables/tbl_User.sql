-- Creating table 'tbl_User'
CREATE TABLE [dbo].[tbl_User] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NULL,
    [Login] nvarchar(255)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [AccessProfileID] uniqueidentifier  NULL,
    [AccessLevelID] int  NOT NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_User'
ALTER TABLE [dbo].[tbl_User]
ADD CONSTRAINT [FK_tbl_User_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_User'
ALTER TABLE [dbo].[tbl_User]
ADD CONSTRAINT [FK_tbl_User_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_User'
ALTER TABLE [dbo].[tbl_User]
ADD CONSTRAINT [PK_tbl_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_User_tbl_Contact'
CREATE INDEX [IX_FK_tbl_User_tbl_Contact]
ON [dbo].[tbl_User]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_User_tbl_Sites'
CREATE INDEX [IX_FK_tbl_User_tbl_Sites]
ON [dbo].[tbl_User]
    ([SiteID]);