-- Creating table 'tbl_ShipmentProducts'
CREATE TABLE [dbo].[tbl_ShipmentProducts] (
    [ID] uniqueidentifier  NOT NULL,
    [ShipmentID] uniqueidentifier  NOT NULL,
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
-- Creating foreign key on [CurrencyID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SpecialOfferPriceListID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer]
    FOREIGN KEY ([SpecialOfferPriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ShipmentID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Shipment]
    FOREIGN KEY ([ShipmentID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UnitID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [PK_tbl_ShipmentProducts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Currency'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Currency]
ON [dbo].[tbl_ShipmentProducts]
    ([CurrencyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_PriceList]
ON [dbo].[tbl_ShipmentProducts]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer]
ON [dbo].[tbl_ShipmentProducts]
    ([SpecialOfferPriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Product'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Product]
ON [dbo].[tbl_ShipmentProducts]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Shipment]
ON [dbo].[tbl_ShipmentProducts]
    ([ShipmentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Task'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Task]
ON [dbo].[tbl_ShipmentProducts]
    ([TaskID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Unit'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Unit]
ON [dbo].[tbl_ShipmentProducts]
    ([UnitID]);