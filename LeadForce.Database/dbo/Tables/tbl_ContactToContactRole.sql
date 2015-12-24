-- Creating table 'tbl_ContactToContactRole'
CREATE TABLE [dbo].[tbl_ContactToContactRole] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactRoleID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [FK_tbl_ContactToContactRole_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactRoleID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [FK_tbl_ContactToContactRole_tbl_ContactRole]
    FOREIGN KEY ([ContactRoleID])
    REFERENCES [dbo].[tbl_ContactRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [FK_tbl_ContactToContactRole_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [PK_tbl_ContactToContactRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactToContactRole_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactToContactRole_tbl_Contact]
ON [dbo].[tbl_ContactToContactRole]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactToContactRole_tbl_ContactRole'
CREATE INDEX [IX_FK_tbl_ContactToContactRole_tbl_ContactRole]
ON [dbo].[tbl_ContactToContactRole]
    ([ContactRoleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactToContactRole_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactToContactRole_tbl_Sites]
ON [dbo].[tbl_ContactToContactRole]
    ([SiteID]);