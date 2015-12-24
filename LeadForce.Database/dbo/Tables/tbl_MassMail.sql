-- Creating table 'tbl_MassMail'
CREATE TABLE [dbo].[tbl_MassMail] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [MailDate] datetime  NULL,
    [SiteActionTemplateID] uniqueidentifier  NOT NULL,
    [MassMailStatusID] int  NOT NULL,
    [FocusGroup] int  NULL,
    [MessageText] nvarchar(2000)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [SiteTagID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_MassMail'
ALTER TABLE [dbo].[tbl_MassMail]
ADD CONSTRAINT [FK_tbl_MassMail_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_MassMail'
ALTER TABLE [dbo].[tbl_MassMail]
ADD CONSTRAINT [FK_tbl_MassMail_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_MassMail'
ALTER TABLE [dbo].[tbl_MassMail]
ADD CONSTRAINT [PK_tbl_MassMail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMail_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_MassMail_tbl_SiteActionTemplate]
ON [dbo].[tbl_MassMail]
    ([SiteActionTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMail_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MassMail_tbl_Sites]
ON [dbo].[tbl_MassMail]
    ([SiteID]);