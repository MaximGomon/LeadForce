-- Creating table 'tbl_PaymentPassRulePass'
CREATE TABLE [dbo].[tbl_PaymentPassRulePass] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentPassRuleID] uniqueidentifier  NULL,
    [OutgoPaymentPassCategoryID] uniqueidentifier  NULL,
    [OutgoCFOID] uniqueidentifier  NULL,
    [OutgoPaymentArticleID] uniqueidentifier  NULL,
    [IncomePaymentPassCategoryID] uniqueidentifier  NULL,
    [IncomeCFOID] uniqueidentifier  NULL,
    [IncomePaymentArticleID] uniqueidentifier  NULL,
    [FormulaID] int  NULL,
    [Value] float  NULL
);
GO
-- Creating foreign key on [OutgoPaymentArticleID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentArticle]
    FOREIGN KEY ([OutgoPaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncomePaymentArticleID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1]
    FOREIGN KEY ([IncomePaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OutgoCFOID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentCFO]
    FOREIGN KEY ([OutgoCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncomeCFOID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1]
    FOREIGN KEY ([IncomeCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncomePaymentPassCategoryID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory]
    FOREIGN KEY ([IncomePaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OutgoPaymentPassCategoryID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1]
    FOREIGN KEY ([OutgoPaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PaymentPassRuleID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass]
    FOREIGN KEY ([PaymentPassRuleID])
    REFERENCES [dbo].[tbl_PaymentPassRule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [PK_tbl_PaymentPassRulePass]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentArticle'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentArticle]
ON [dbo].[tbl_PaymentPassRulePass]
    ([OutgoPaymentArticleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1]
ON [dbo].[tbl_PaymentPassRulePass]
    ([IncomePaymentArticleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentCFO'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentCFO]
ON [dbo].[tbl_PaymentPassRulePass]
    ([OutgoCFOID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1]
ON [dbo].[tbl_PaymentPassRulePass]
    ([IncomeCFOID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentPassRulePass]
    ([IncomePaymentPassCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1]
ON [dbo].[tbl_PaymentPassRulePass]
    ([OutgoPaymentPassCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass]
ON [dbo].[tbl_PaymentPassRulePass]
    ([PaymentPassRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_Sites]
ON [dbo].[tbl_PaymentPassRulePass]
    ([SiteID]);