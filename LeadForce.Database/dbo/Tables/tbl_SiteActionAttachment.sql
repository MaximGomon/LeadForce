-- Creating table 'tbl_SiteActionAttachment'
CREATE TABLE [dbo].[tbl_SiteActionAttachment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NOT NULL,
    [FileName] nvarchar(255)  NOT NULL
);
GO
-- Creating foreign key on [SiteActionID] in table 'tbl_SiteActionAttachment'
ALTER TABLE [dbo].[tbl_SiteActionAttachment]
ADD CONSTRAINT [FK_tbl_SiteActionAttachment_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActionAttachment'
ALTER TABLE [dbo].[tbl_SiteActionAttachment]
ADD CONSTRAINT [FK_tbl_SiteActionAttachment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActionAttachment'
ALTER TABLE [dbo].[tbl_SiteActionAttachment]
ADD CONSTRAINT [PK_tbl_SiteActionAttachment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionAttachment_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_SiteActionAttachment_tbl_SiteAction]
ON [dbo].[tbl_SiteActionAttachment]
    ([SiteActionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionAttachment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActionAttachment_tbl_Sites]
ON [dbo].[tbl_SiteActionAttachment]
    ([SiteID]);