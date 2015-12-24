-- Creating table 'tbl_Order'
CREATE TABLE [dbo].[tbl_Order] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(255)  NOT NULL,
    [SerialNumber] int  NULL,
    [CreatedAt] datetime  NOT NULL,
    [OrderTypeID] uniqueidentifier  NOT NULL,
    [OrderStatusID] int  NOT NULL,
    [Note] nvarchar(2000)  NULL,
    [BuyerCompanyID] uniqueidentifier  NULL,
    [BuyerContactID] uniqueidentifier  NULL,
    [ExecutorCompanyID] uniqueidentifier  NULL,
    [ExecutorContactID] uniqueidentifier  NULL,
    [Ordered] decimal(19,4)  NOT NULL,
    [Paid] decimal(19,4)  NOT NULL,
    [Shipped] decimal(19,4)  NOT NULL,
    [ExpirationDateBegin] datetime  NULL,
    [ExpirationDateEnd] datetime  NULL,
    [PlannedDeliveryDate] datetime  NULL,
    [ActualDeliveryDate] datetime  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [PriceListID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [BuyerCompanyID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_CompanyBuyer]
    FOREIGN KEY ([BuyerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorCompanyID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_CompanyExecutor]
    FOREIGN KEY ([ExecutorCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [BuyerContactID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_ContactBuyer]
    FOREIGN KEY ([BuyerContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorContactID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_ContactExecutor]
    FOREIGN KEY ([ExecutorContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderStatusID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_OrderStatus]
    FOREIGN KEY ([OrderStatusID])
    REFERENCES [dbo].[tbl_OrderStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderTypeID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_OrderType]
    FOREIGN KEY ([OrderTypeID])
    REFERENCES [dbo].[tbl_OrderType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [PK_tbl_Order]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_CompanyBuyer'
CREATE INDEX [IX_FK_tbl_Order_tbl_CompanyBuyer]
ON [dbo].[tbl_Order]
    ([BuyerCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_CompanyExecutor'
CREATE INDEX [IX_FK_tbl_Order_tbl_CompanyExecutor]
ON [dbo].[tbl_Order]
    ([ExecutorCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_ContactBuyer'
CREATE INDEX [IX_FK_tbl_Order_tbl_ContactBuyer]
ON [dbo].[tbl_Order]
    ([BuyerContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_ContactExecutor'
CREATE INDEX [IX_FK_tbl_Order_tbl_ContactExecutor]
ON [dbo].[tbl_Order]
    ([ExecutorContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_OrderStatus'
CREATE INDEX [IX_FK_tbl_Order_tbl_OrderStatus]
ON [dbo].[tbl_Order]
    ([OrderStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_OrderType'
CREATE INDEX [IX_FK_tbl_Order_tbl_OrderType]
ON [dbo].[tbl_Order]
    ([OrderTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Order_tbl_PriceList]
ON [dbo].[tbl_Order]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Order_tbl_Sites]
ON [dbo].[tbl_Order]
    ([SiteID]);