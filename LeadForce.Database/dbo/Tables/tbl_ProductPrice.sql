-- Creating table 'tbl_ProductPrice'
CREATE TABLE [dbo].[tbl_ProductPrice] (
    [ID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [PriceListID] uniqueidentifier  NOT NULL,
    [SupplierID] uniqueidentifier  NULL,
    [DateFrom] datetime  NULL,
    [DateTo] datetime  NULL,
    [QuantityFrom] float  NULL,
    [QuantityTo] float  NULL,
    [Discount] float  NULL,
    [Price] float  NULL
);
GO
-- Creating foreign key on [SupplierID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [FK_tbl_ProductPrice_tbl_Company]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SupplierID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [FK_tbl_ProductPrice_tbl_Product]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [FK_tbl_ProductPrice_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [PK_tbl_ProductPrice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPrice_tbl_Company'
CREATE INDEX [IX_FK_tbl_ProductPrice_tbl_Company]
ON [dbo].[tbl_ProductPrice]
    ([SupplierID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPrice_tbl_Product'
CREATE INDEX [IX_FK_tbl_ProductPrice_tbl_Product]
ON [dbo].[tbl_ProductPrice]
    ([SupplierID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPrice_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_ProductPrice_tbl_PriceList]
ON [dbo].[tbl_ProductPrice]
    ([PriceListID]);