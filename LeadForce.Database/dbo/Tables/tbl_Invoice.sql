-- Creating table 'tbl_Invoice'
CREATE TABLE [dbo].[tbl_Invoice] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [InvoiceTypeID] uniqueidentifier  NOT NULL,
    [InvoiceStatusID] int  NOT NULL,
    [Note] nvarchar(1024)  NULL,
    [BuyerCompanyID] uniqueidentifier  NULL,
    [BuyerCompanyLegalAccountID] uniqueidentifier  NULL,
    [BuyerContactID] uniqueidentifier  NULL,
    [ExecutorCompanyID] uniqueidentifier  NULL,
    [ExecutorCompanyLegalAccountID] uniqueidentifier  NULL,
    [ExecutorContactID] uniqueidentifier  NULL,
    [InvoiceAmount] decimal(19,4)  NOT NULL,
    [Paid] decimal(19,4)  NOT NULL,
    [PaymentDatePlanned] datetime  NULL,
    [PaymentDateActual] datetime  NULL,
    [OrderID] uniqueidentifier  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [IsExistBuyerComplaint] bit  NOT NULL,
    [IsPaymentDateFixedByContract] bit  NOT NULL,
    [ModifiedAt] datetime  NULL
);
GO
-- Creating foreign key on [BuyerCompanyID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Company_Buyer]
    FOREIGN KEY ([BuyerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorCompanyID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Company_Executor]
    FOREIGN KEY ([ExecutorCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [BuyerCompanyLegalAccountID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer]
    FOREIGN KEY ([BuyerCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorCompanyLegalAccountID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor]
    FOREIGN KEY ([ExecutorCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OwnerID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [BuyerContactID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Contact_Buyer]
    FOREIGN KEY ([BuyerContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorContactID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Contact_Executor]
    FOREIGN KEY ([ExecutorContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceStatusID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_InvoiceStatus]
    FOREIGN KEY ([InvoiceStatusID])
    REFERENCES [dbo].[tbl_InvoiceStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [PK_tbl_Invoice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Company_Buyer'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Company_Buyer]
ON [dbo].[tbl_Invoice]
    ([BuyerCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Company_Executor'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Company_Executor]
ON [dbo].[tbl_Invoice]
    ([ExecutorCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer]
ON [dbo].[tbl_Invoice]
    ([BuyerCompanyLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor]
ON [dbo].[tbl_Invoice]
    ([ExecutorCompanyLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Contact]
ON [dbo].[tbl_Invoice]
    ([OwnerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Contact_Buyer'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Contact_Buyer]
ON [dbo].[tbl_Invoice]
    ([BuyerContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Contact_Executor'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Contact_Executor]
ON [dbo].[tbl_Invoice]
    ([ExecutorContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_InvoiceStatus'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_InvoiceStatus]
ON [dbo].[tbl_Invoice]
    ([InvoiceStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Order'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Order]
ON [dbo].[tbl_Invoice]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_PriceList]
ON [dbo].[tbl_Invoice]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Sites]
ON [dbo].[tbl_Invoice]
    ([SiteID]);