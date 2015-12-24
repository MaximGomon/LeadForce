-- Creating table 'tbl_OrderProducts'
CREATE TABLE [dbo].[tbl_OrderProducts] (
    [ID] uniqueidentifier  NOT NULL,
    [OrderID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [SerialNumber] nvarchar(255)  NULL,
    [PriceListID] uniqueidentifier  NOT NULL,
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
    [ParentOrderProductID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [CurrencyID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ParentOrderProductID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_OrderProductsParent]
    FOREIGN KEY ([ParentOrderProductID])
    REFERENCES [dbo].[tbl_OrderProducts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SpecialOfferPriceListID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_PriceListSpecialOffer]
    FOREIGN KEY ([SpecialOfferPriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [PK_tbl_OrderProducts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_Currency'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_Currency]
ON [dbo].[tbl_OrderProducts]
    ([CurrencyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_Order'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_Order]
ON [dbo].[tbl_OrderProducts]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_OrderProductsParent'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_OrderProductsParent]
ON [dbo].[tbl_OrderProducts]
    ([ParentOrderProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_PriceList]
ON [dbo].[tbl_OrderProducts]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_PriceListSpecialOffer'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_PriceListSpecialOffer]
ON [dbo].[tbl_OrderProducts]
    ([SpecialOfferPriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_Product'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_Product]
ON [dbo].[tbl_OrderProducts]
    ([ProductID]);