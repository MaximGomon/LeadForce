-- Creating table 'tbl_SiteActionTemplateRecipient'
CREATE TABLE [dbo].[tbl_SiteActionTemplateRecipient] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActionTemplateID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NULL,
    [ContactRoleID] uniqueidentifier  NULL,
    [Email] nvarchar(255)  NULL,
    [DisplayName] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_SiteActionTemplateRecipient'
ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateRecipient_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteActionTemplateRecipient'
ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActionTemplateRecipient'
ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient]
ADD CONSTRAINT [PK_tbl_SiteActionTemplateRecipient]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateRecipient_tbl_Contact'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateRecipient_tbl_Contact]
ON [dbo].[tbl_SiteActionTemplateRecipient]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteActionTemplateRecipient]
    ([SiteActionTemplateID]);