-- Creating table 'tbl_ShipmentHistory'
CREATE TABLE [dbo].[tbl_ShipmentHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ShipmentID] uniqueidentifier  NOT NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [SendDate] datetime  NULL,
    [ShipmentAmount] decimal(19,4)  NOT NULL,
    [ShipmentStatusID] int  NOT NULL,
    [Note] nvarchar(1024)  NULL
);
GO
-- Creating foreign key on [AuthorID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [FK_tbl_ShipmentHistory_tbl_Contact]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ShipmentID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [FK_tbl_ShipmentHistory_tbl_Shipment]
    FOREIGN KEY ([ShipmentID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ShipmentStatusID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [FK_tbl_ShipmentHistory_tbl_ShipmentStatus]
    FOREIGN KEY ([ShipmentStatusID])
    REFERENCES [dbo].[tbl_ShipmentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [PK_tbl_ShipmentHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ShipmentHistory_tbl_Contact]
ON [dbo].[tbl_ShipmentHistory]
    ([AuthorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentHistory_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_ShipmentHistory_tbl_Shipment]
ON [dbo].[tbl_ShipmentHistory]
    ([ShipmentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentHistory_tbl_ShipmentStatus'
CREATE INDEX [IX_FK_tbl_ShipmentHistory_tbl_ShipmentStatus]
ON [dbo].[tbl_ShipmentHistory]
    ([ShipmentStatusID]);