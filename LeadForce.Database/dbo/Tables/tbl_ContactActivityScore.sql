-- Creating table 'tbl_ContactActivityScore'
CREATE TABLE [dbo].[tbl_ContactActivityScore] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityScoreTypeID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [Score] int  NOT NULL,
    [ScoreCategory] int  NOT NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [FK_tbl_ContactScore_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteActivityScoreTypeID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType]
    FOREIGN KEY ([SiteActivityScoreTypeID])
    REFERENCES [dbo].[tbl_SiteActivityScoreType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [FK_tbl_ContactActivityScore_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [PK_tbl_ContactActivityScore]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactScore_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactScore_tbl_Contact]
ON [dbo].[tbl_ContactActivityScore]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType'
CREATE INDEX [IX_FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType]
ON [dbo].[tbl_ContactActivityScore]
    ([SiteActivityScoreTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivityScore_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactActivityScore_tbl_Sites]
ON [dbo].[tbl_ContactActivityScore]
    ([SiteID]);