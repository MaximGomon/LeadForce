-- Creating table 'tbl_PaymentPass'
CREATE TABLE [dbo].[tbl_PaymentPass] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentID] uniqueidentifier  NULL,
    [CreatedAt] datetime  NULL,
    [OutgoPaymentPassCategoryID] uniqueidentifier  NULL,
    [OutgoCFOID] uniqueidentifier  NULL,
    [OutgoPaymentArticleID] uniqueidentifier  NULL,
    [IncomePaymentPassCategoryID] uniqueidentifier  NULL,
    [IncomeCFOID] uniqueidentifier  NULL,
    [IncomePaymentArticleID] uniqueidentifier  NULL,
    [FormulaID] int  NULL,
    [Value] float  NULL,
    [Amount] float  NULL,
    [ProcessedByCron] bit  NOT NULL,
    [ToDelete] bit  NOT NULL,
    [OldOutgoPaymentPassCategoryID] uniqueidentifier  NULL,
    [OldOutgoCFOID] uniqueidentifier  NULL,
    [OldOutgoPaymentArticleID] uniqueidentifier  NULL,
    [OldAmount] float  NULL,
    [OldCreatedAt] datetime  NULL,
    [IsFact] bit  NULL
);
GO
-- Creating foreign key on [PaymentID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_Payment]
    FOREIGN KEY ([PaymentID])
    REFERENCES [dbo].[tbl_Payment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OutgoPaymentArticleID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentArticle]
    FOREIGN KEY ([OutgoPaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncomePaymentArticleID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentArticle1]
    FOREIGN KEY ([IncomePaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OutgoCFOID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentCFO]
    FOREIGN KEY ([OutgoCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncomeCFOID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentCFO1]
    FOREIGN KEY ([IncomeCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OutgoPaymentPassCategoryID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentPassCategory]
    FOREIGN KEY ([OutgoPaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncomePaymentPassCategoryID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentPassCategory1]
    FOREIGN KEY ([IncomePaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [PK_tbl_PaymentPass]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_Payment'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_Payment]
ON [dbo].[tbl_PaymentPass]
    ([PaymentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentArticle'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentArticle]
ON [dbo].[tbl_PaymentPass]
    ([OutgoPaymentArticleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentArticle1'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentArticle1]
ON [dbo].[tbl_PaymentPass]
    ([IncomePaymentArticleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentCFO'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentCFO]
ON [dbo].[tbl_PaymentPass]
    ([OutgoCFOID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentCFO1'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentCFO1]
ON [dbo].[tbl_PaymentPass]
    ([IncomeCFOID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentPass]
    ([OutgoPaymentPassCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentPassCategory1'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentPassCategory1]
ON [dbo].[tbl_PaymentPass]
    ([IncomePaymentPassCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_Sites]
ON [dbo].[tbl_PaymentPass]
    ([SiteID]);