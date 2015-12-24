-- Creating table 'tbl_EmailStatsUnsubscribe'
CREATE TABLE [dbo].[tbl_EmailStatsUnsubscribe] (
    [EmailStatsID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO
-- Creating foreign key on [EmailStatsID] in table 'tbl_EmailStatsUnsubscribe'
ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe]
ADD CONSTRAINT [FK_tbl_EmailStatsUnsubscribe_tbl_EmailStats]
    FOREIGN KEY ([EmailStatsID])
    REFERENCES [dbo].[tbl_EmailStats]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_EmailStatsUnsubscribe'
ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe]
ADD CONSTRAINT [FK_tbl_EmailStatsUnsubscribe_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [EmailStatsID], [SiteID] in table 'tbl_EmailStatsUnsubscribe'
ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe]
ADD CONSTRAINT [PK_tbl_EmailStatsUnsubscribe]
    PRIMARY KEY CLUSTERED ([EmailStatsID], [SiteID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_EmailStatsUnsubscribe_tbl_Sites'
CREATE INDEX [IX_FK_tbl_EmailStatsUnsubscribe_tbl_Sites]
ON [dbo].[tbl_EmailStatsUnsubscribe]
    ([SiteID]);