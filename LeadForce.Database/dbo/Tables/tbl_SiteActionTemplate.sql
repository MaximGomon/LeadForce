-- Creating table 'tbl_SiteActionTemplate'
CREATE TABLE [dbo].[tbl_SiteActionTemplate] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [ActionTypeID] int  NOT NULL,
    [FromEmail] nvarchar(255)  NULL,
    [ToEmail] nvarchar(255)  NULL,
    [MessageCaption] nvarchar(255)  NULL,
    [MessageBody] nvarchar(max)  NULL,
    [FromName] nvarchar(255)  NULL,
    [ReplyToEmail] nvarchar(255)  NULL,
    [ReplyToName] nvarchar(255)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [SystemName] nvarchar(50)  NULL,
    [ReplaceLinksID] int  NOT NULL,
    [SiteActionTemplateCategoryID] int  NULL,
    [ParentID] uniqueidentifier  NULL,
    [UsageID] uniqueidentifier  NULL,
    [FromContactRoleID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ActionTypeID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteActionTemplate_tbl_ActionTypes]
    FOREIGN KEY ([ActionTypeID])
    REFERENCES [dbo].[tbl_ActionTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteActionTemplate_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ParentID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [PK_tbl_SiteActionTemplate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplate_tbl_ActionTypes'
CREATE INDEX [IX_FK_tbl_SiteActionTemplate_tbl_ActionTypes]
ON [dbo].[tbl_SiteActionTemplate]
    ([ActionTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplate_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActionTemplate_tbl_Sites]
ON [dbo].[tbl_SiteActionTemplate]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent'
CREATE INDEX [IX_FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent]
ON [dbo].[tbl_SiteActionTemplate]
    ([ParentID]);