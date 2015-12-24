-- Creating table 'tbl_PaymentBalance'
CREATE TABLE [dbo].[tbl_PaymentBalance] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentPassCategoryID] uniqueidentifier  NULL,
    [CFOID] uniqueidentifier  NULL,
    [PaymentArticleID] uniqueidentifier  NULL,
    [Date] datetime  NULL,
    [BalancePlan] decimal(19,4)  NULL,
    [BalanceFact] decimal(19,4)  NULL
);
GO
-- Creating foreign key on [PaymentArticleID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentArticle]
    FOREIGN KEY ([PaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PaymentPassCategoryID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentBalance]
    FOREIGN KEY ([PaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CFOID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentCFO]
    FOREIGN KEY ([CFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [PK_tbl_PaymentBalance]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_PaymentArticle'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_PaymentArticle]
ON [dbo].[tbl_PaymentBalance]
    ([PaymentArticleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_PaymentBalance'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_PaymentBalance]
ON [dbo].[tbl_PaymentBalance]
    ([PaymentPassCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_PaymentCFO'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_PaymentCFO]
ON [dbo].[tbl_PaymentBalance]
    ([CFOID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_Sites]
ON [dbo].[tbl_PaymentBalance]
    ([SiteID]);