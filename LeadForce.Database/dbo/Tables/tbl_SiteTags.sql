-- Creating table 'tbl_SiteTags'
CREATE TABLE [dbo].[tbl_SiteTags] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ObjectTypeID] int  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Description] nvarchar(250)  NULL
);
GO
-- Creating foreign key on [ObjectTypeID] in table 'tbl_SiteTags'
ALTER TABLE [dbo].[tbl_SiteTags]
ADD CONSTRAINT [FK_tbl_SiteTags_tbl_ObjectTypes]
    FOREIGN KEY ([ObjectTypeID])
    REFERENCES [dbo].[tbl_ObjectTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_SiteTags'
ALTER TABLE [dbo].[tbl_SiteTags]
ADD CONSTRAINT [FK_tbl_SiteTags_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteTags'
ALTER TABLE [dbo].[tbl_SiteTags]
ADD CONSTRAINT [PK_tbl_SiteTags]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteTags_tbl_ObjectTypes'
CREATE INDEX [IX_FK_tbl_SiteTags_tbl_ObjectTypes]
ON [dbo].[tbl_SiteTags]
    ([ObjectTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteTags_tbl_User'
CREATE INDEX [IX_FK_tbl_SiteTags_tbl_User]
ON [dbo].[tbl_SiteTags]
    ([UserID]);