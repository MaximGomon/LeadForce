-- Creating table 'tbl_ContactActivityScoreHistory'
CREATE TABLE [dbo].[tbl_ContactActivityScoreHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactActivityScoreID] uniqueidentifier  NOT NULL,
    [Operation] int  NOT NULL,
    [Score] int  NOT NULL,
    [ActivityDate] datetime  NOT NULL,
    [Comment] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [ContactActivityScoreID] in table 'tbl_ContactActivityScoreHistory'
ALTER TABLE [dbo].[tbl_ContactActivityScoreHistory]
ADD CONSTRAINT [FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore]
    FOREIGN KEY ([ContactActivityScoreID])
    REFERENCES [dbo].[tbl_ContactActivityScore]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactActivityScoreHistory'
ALTER TABLE [dbo].[tbl_ContactActivityScoreHistory]
ADD CONSTRAINT [PK_tbl_ContactActivityScoreHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore'
CREATE INDEX [IX_FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore]
ON [dbo].[tbl_ContactActivityScoreHistory]
    ([ContactActivityScoreID]);