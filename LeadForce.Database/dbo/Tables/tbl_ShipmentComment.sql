-- Creating table 'tbl_ShipmentComment'
CREATE TABLE [dbo].[tbl_ShipmentComment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(256)  NULL,
    [IsOfficialAnswer] bit  NULL,
    [DestinationUserID] uniqueidentifier  NULL,
    [ReplyToID] uniqueidentifier  NULL,
    [IsInternal] bit  NOT NULL
);
GO
-- Creating foreign key on [ContentID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_Shipment]
    FOREIGN KEY ([ContentID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ReplyToID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_ShipmentComment]
    FOREIGN KEY ([ReplyToID])
    REFERENCES [dbo].[tbl_ShipmentComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [DestinationUserID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_User_Destination]
    FOREIGN KEY ([DestinationUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [PK_tbl_ShipmentComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_Shipment]
ON [dbo].[tbl_ShipmentComment]
    ([ContentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_ShipmentComment'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_ShipmentComment]
ON [dbo].[tbl_ShipmentComment]
    ([ReplyToID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_Sites]
ON [dbo].[tbl_ShipmentComment]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_User'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_User]
ON [dbo].[tbl_ShipmentComment]
    ([UserID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_User_Destination'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_User_Destination]
ON [dbo].[tbl_ShipmentComment]
    ([DestinationUserID]);