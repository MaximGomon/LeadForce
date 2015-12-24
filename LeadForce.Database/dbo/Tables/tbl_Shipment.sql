-- Creating table 'tbl_Shipment'
CREATE TABLE [dbo].[tbl_Shipment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ShipmentTypeID] uniqueidentifier  NOT NULL,
    [ShipmentStatusID] int  NOT NULL,
    [Note] nvarchar(1024)  NULL,
    [BuyerCompanyID] uniqueidentifier  NULL,
    [BuyerCompanyLegalAccountID] uniqueidentifier  NULL,
    [BuyerContactID] uniqueidentifier  NULL,
    [ExecutorCompanyID] uniqueidentifier  NULL,
    [ExecutorCompanyLegalAccountID] uniqueidentifier  NULL,
    [ExecutorContactID] uniqueidentifier  NULL,
    [ShipmentAmount] decimal(19,4)  NOT NULL,
    [SendDate] datetime  NULL,
    [OrderID] uniqueidentifier  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [ModifiedAt] datetime  NULL
);
GO
-- Creating foreign key on [BuyerCompanyID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Company_Buyer]
    FOREIGN KEY ([BuyerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorCompanyID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Company_Executor]
    FOREIGN KEY ([ExecutorCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [BuyerCompanyLegalAccountID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer]
    FOREIGN KEY ([BuyerCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorCompanyLegalAccountID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor]
    FOREIGN KEY ([ExecutorCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OwnerID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [BuyerContactID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Contact_Buyer]
    FOREIGN KEY ([BuyerContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ExecutorContactID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Contact_Executor]
    FOREIGN KEY ([ExecutorContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ShipmentStatusID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_ShipmentStatus]
    FOREIGN KEY ([ShipmentStatusID])
    REFERENCES [dbo].[tbl_ShipmentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ShipmentTypeID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_ShipmentType]
    FOREIGN KEY ([ShipmentTypeID])
    REFERENCES [dbo].[tbl_ShipmentType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [PK_tbl_Shipment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Company_Buyer'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Company_Buyer]
ON [dbo].[tbl_Shipment]
    ([BuyerCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Company_Executor'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Company_Executor]
ON [dbo].[tbl_Shipment]
    ([ExecutorCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer]
ON [dbo].[tbl_Shipment]
    ([BuyerCompanyLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor]
ON [dbo].[tbl_Shipment]
    ([ExecutorCompanyLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Contact]
ON [dbo].[tbl_Shipment]
    ([OwnerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Contact_Buyer'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Contact_Buyer]
ON [dbo].[tbl_Shipment]
    ([BuyerContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Contact_Executor'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Contact_Executor]
ON [dbo].[tbl_Shipment]
    ([ExecutorContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Order'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Order]
ON [dbo].[tbl_Shipment]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_PriceList]
ON [dbo].[tbl_Shipment]
    ([PriceListID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_ShipmentStatus'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_ShipmentStatus]
ON [dbo].[tbl_Shipment]
    ([ShipmentStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_ShipmentType'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_ShipmentType]
ON [dbo].[tbl_Shipment]
    ([ShipmentTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Sites]
ON [dbo].[tbl_Shipment]
    ([SiteID]);