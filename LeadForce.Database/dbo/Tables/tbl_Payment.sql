-- Creating table 'tbl_Payment'
CREATE TABLE [dbo].[tbl_Payment] (
    [ID] uniqueidentifier  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Assignment] nvarchar(250)  NOT NULL,
    [DatePlan] datetime  NOT NULL,
    [DateFact] datetime  NULL,
    [PaymentTypeID] int  NULL,
    [StatusID] uniqueidentifier  NULL,
    [PayerID] uniqueidentifier  NULL,
    [PayerLegalAccountID] uniqueidentifier  NULL,
    [RecipientID] uniqueidentifier  NULL,
    [RecipientLegalAccountID] uniqueidentifier  NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [Course] float  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Total] decimal(19,4)  NOT NULL,
    [PaymentPassRuleID] uniqueidentifier  NULL,
    [OrderID] uniqueidentifier  NULL,
    [InvoiceID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [PayerID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Company]
    FOREIGN KEY ([PayerID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RecipientID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Company1]
    FOREIGN KEY ([RecipientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PayerLegalAccountID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_CompanyLegalAccount]
    FOREIGN KEY ([PayerLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RecipientLegalAccountID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_CompanyLegalAccount1]
    FOREIGN KEY ([RecipientLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CurrencyID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PaymentPassRuleID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_PaymentPassRule]
    FOREIGN KEY ([PaymentPassRuleID])
    REFERENCES [dbo].[tbl_PaymentPassRule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [StatusID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_PaymentStatus]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PaymentTypeID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_PaymentType]
    FOREIGN KEY ([PaymentTypeID])
    REFERENCES [dbo].[tbl_PaymentType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [PK_tbl_Payment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Company'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Company]
ON [dbo].[tbl_Payment]
    ([PayerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Company1'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Company1]
ON [dbo].[tbl_Payment]
    ([RecipientID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_CompanyLegalAccount'
CREATE INDEX [IX_FK_tbl_Payment_tbl_CompanyLegalAccount]
ON [dbo].[tbl_Payment]
    ([PayerLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_CompanyLegalAccount1'
CREATE INDEX [IX_FK_tbl_Payment_tbl_CompanyLegalAccount1]
ON [dbo].[tbl_Payment]
    ([RecipientLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Currency'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Currency]
ON [dbo].[tbl_Payment]
    ([CurrencyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Invoice]
ON [dbo].[tbl_Payment]
    ([InvoiceID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Order'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Order]
ON [dbo].[tbl_Payment]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_PaymentPassRule'
CREATE INDEX [IX_FK_tbl_Payment_tbl_PaymentPassRule]
ON [dbo].[tbl_Payment]
    ([PaymentPassRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_PaymentStatus'
CREATE INDEX [IX_FK_tbl_Payment_tbl_PaymentStatus]
ON [dbo].[tbl_Payment]
    ([StatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_PaymentType'
CREATE INDEX [IX_FK_tbl_Payment_tbl_PaymentType]
ON [dbo].[tbl_Payment]
    ([PaymentTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Sites]
ON [dbo].[tbl_Payment]
    ([SiteID]);