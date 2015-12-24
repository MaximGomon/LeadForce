-- Creating table 'tbl_SiteActivityScoreType'
CREATE TABLE [dbo].[tbl_SiteActivityScoreType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityScoreType'
ALTER TABLE [dbo].[tbl_SiteActivityScoreType]
ADD CONSTRAINT [FK_tbl_SiteActivityScoreType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityScoreType'
ALTER TABLE [dbo].[tbl_SiteActivityScoreType]
ADD CONSTRAINT [PK_tbl_SiteActivityScoreType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityScoreType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityScoreType_tbl_Sites]
ON [dbo].[tbl_SiteActivityScoreType]
    ([SiteID]);