-- Creating table 'tbl_MassMailContact'
CREATE TABLE [dbo].[tbl_MassMailContact] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [MassMailID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [MassMailID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_MassMail]
    FOREIGN KEY ([MassMailID])
    REFERENCES [dbo].[tbl_MassMail]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActionID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [PK_tbl_MassMailContact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_Contact'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_Contact]
ON [dbo].[tbl_MassMailContact]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_MassMail'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_MassMail]
ON [dbo].[tbl_MassMailContact]
    ([MassMailID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_SiteAction]
ON [dbo].[tbl_MassMailContact]
    ([SiteActionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_Sites]
ON [dbo].[tbl_MassMailContact]
    ([SiteID]);