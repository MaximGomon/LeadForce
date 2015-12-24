-- Creating table 'tbl_SiteActionLink'
CREATE TABLE [dbo].[tbl_SiteActionLink] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NULL,
    [SiteActionTemplateID] uniqueidentifier  NULL,
    [SiteActivityRuleID] uniqueidentifier  NULL,
    [LinkURL] nvarchar(2000)  NULL,
    [ActionLinkDate] datetime  NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_Links]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_Links]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActionID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [PK_tbl_SiteActionLink]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_Contact'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_Contact]
ON [dbo].[tbl_SiteActionLink]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_Links'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_Links]
ON [dbo].[tbl_SiteActionLink]
    ([SiteActivityRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_SiteAction]
ON [dbo].[tbl_SiteActionLink]
    ([SiteActionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteActionLink]
    ([SiteActionTemplateID]);