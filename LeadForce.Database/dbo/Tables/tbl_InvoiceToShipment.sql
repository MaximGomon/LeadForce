-- Creating table 'tbl_InvoiceToShipment'
CREATE TABLE [dbo].[tbl_InvoiceToShipment] (
    [tbl_Invoice_ID] uniqueidentifier  NOT NULL,
    [tbl_Shipment_ID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [tbl_Invoice_ID] in table 'tbl_InvoiceToShipment'
ALTER TABLE [dbo].[tbl_InvoiceToShipment]
ADD CONSTRAINT [FK_tbl_InvoiceToShipment_tbl_Invoice]
    FOREIGN KEY ([tbl_Invoice_ID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [tbl_Shipment_ID] in table 'tbl_InvoiceToShipment'
ALTER TABLE [dbo].[tbl_InvoiceToShipment]
ADD CONSTRAINT [FK_tbl_InvoiceToShipment_tbl_Shipment]
    FOREIGN KEY ([tbl_Shipment_ID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [tbl_Invoice_ID], [tbl_Shipment_ID] in table 'tbl_InvoiceToShipment'
ALTER TABLE [dbo].[tbl_InvoiceToShipment]
ADD CONSTRAINT [PK_tbl_InvoiceToShipment]
    PRIMARY KEY CLUSTERED ([tbl_Invoice_ID], [tbl_Shipment_ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceToShipment_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_InvoiceToShipment_tbl_Shipment]
ON [dbo].[tbl_InvoiceToShipment]
    ([tbl_Shipment_ID]);