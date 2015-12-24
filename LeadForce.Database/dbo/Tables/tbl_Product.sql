-- Creating table 'tbl_Product'
CREATE TABLE [dbo].[tbl_Product] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [SKU] nvarchar(250)  NULL,
    [ProductStatusID] int  NULL,
    [ProductCategoryID] uniqueidentifier  NULL,
    [BrandID] uniqueidentifier  NULL,
    [ProductTypeID] uniqueidentifier  NULL,
    [UnitID] uniqueidentifier  NULL,
    [Price] decimal(19,4)  NULL,
    [WholesalePrice] decimal(19,4)  NULL,
    [CostPrice] decimal(19,4)  NULL,
    [SupplierID] uniqueidentifier  NULL,
    [SupplierSKU] nvarchar(250)  NULL,
    [ManufacturerID] uniqueidentifier  NULL,
    [CountryID] uniqueidentifier  NULL,
    [Description] nvarchar(max)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [ModifiedAt] datetime  NULL,
    [CreatedAt] datetime  NULL
);
GO
-- Creating foreign key on [SupplierID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Company]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ManufacturerID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Company1]
    FOREIGN KEY ([ManufacturerID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CountryID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductStatusID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_ProductStatus]
    FOREIGN KEY ([ProductStatusID])
    REFERENCES [dbo].[tbl_ProductStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductTypeID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_ProductType]
    FOREIGN KEY ([ProductTypeID])
    REFERENCES [dbo].[tbl_ProductType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UnitID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [PK_tbl_Product]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Company'
CREATE INDEX [IX_FK_tbl_Product_tbl_Company]
ON [dbo].[tbl_Product]
    ([SupplierID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Company1'
CREATE INDEX [IX_FK_tbl_Product_tbl_Company1]
ON [dbo].[tbl_Product]
    ([ManufacturerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Country'
CREATE INDEX [IX_FK_tbl_Product_tbl_Country]
ON [dbo].[tbl_Product]
    ([CountryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_ProductStatus'
CREATE INDEX [IX_FK_tbl_Product_tbl_ProductStatus]
ON [dbo].[tbl_Product]
    ([ProductStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_ProductType'
CREATE INDEX [IX_FK_tbl_Product_tbl_ProductType]
ON [dbo].[tbl_Product]
    ([ProductTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Product_tbl_Sites]
ON [dbo].[tbl_Product]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Unit'
CREATE INDEX [IX_FK_tbl_Product_tbl_Unit]
ON [dbo].[tbl_Product]
    ([UnitID]);