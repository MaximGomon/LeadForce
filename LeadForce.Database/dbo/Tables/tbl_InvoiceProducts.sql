-- Creating table 'tbl_InvoiceProducts'
CREATE TABLE [dbo].[tbl_InvoiceProducts] (
    [ID] uniqueidentifier  NOT NULL,
    [InvoiceID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [SerialNumber] nvarchar(255)  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [UnitID] uniqueidentifier  NOT NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [Rate] decimal(19,4)  NOT NULL,
    [CurrencyPrice] decimal(19,4)  NOT NULL,
    [CurrencyAmount] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [SpecialOfferPriceListID] uniqueidentifier  NULL,
    [Discount] decimal(18,4)  NULL,
    [CurrencyDiscountAmount] decimal(19,4)  NULL,
    [DiscountAmount] decimal(19,4)  NULL,
    [CurrencyTotalAmount] decimal(19,4)  NOT NULL,
    [TotalAmount] decimal(19,4)  NOT NULL,
    [TaskID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [CurrencyID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SpecialOfferPriceListID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer]
    FOREIGN KEY ([SpecialOfferPriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UnitID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [PK_tbl_InvoiceProducts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Currency'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Currency]
ON [dbo].[tbl_InvoiceProducts]
    ([CurrencyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Invoice]
ON [dbo].[tbl_InvoiceProducts]
    ([InvoiceID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_PriceList]
ON [dbo].[tbl_InvoiceProducts]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer]
ON [dbo].[tbl_InvoiceProducts]
    ([SpecialOfferPriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Product'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Product]
ON [dbo].[tbl_InvoiceProducts]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Task'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Task]
ON [dbo].[tbl_InvoiceProducts]
    ([TaskID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Unit'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Unit]
ON [dbo].[tbl_InvoiceProducts]
    ([UnitID]);