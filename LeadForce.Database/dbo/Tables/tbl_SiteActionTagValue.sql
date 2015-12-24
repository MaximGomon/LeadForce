-- Creating table 'tbl_SiteActionTagValue'
CREATE TABLE [dbo].[tbl_SiteActionTagValue] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NOT NULL,
    [Tag] nvarchar(256)  NOT NULL,
    [Value] nvarchar(2048)  NOT NULL
);
GO
-- Creating foreign key on [SiteActionID] in table 'tbl_SiteActionTagValue'
ALTER TABLE [dbo].[tbl_SiteActionTagValue]
ADD CONSTRAINT [FK_tbl_SiteActionTagValue_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActionTagValue'
ALTER TABLE [dbo].[tbl_SiteActionTagValue]
ADD CONSTRAINT [PK_tbl_SiteActionTagValue]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTagValue_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_SiteActionTagValue_tbl_SiteAction]
ON [dbo].[tbl_SiteActionTagValue]
    ([SiteActionID]);