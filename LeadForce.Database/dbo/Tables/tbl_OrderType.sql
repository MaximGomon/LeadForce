-- Creating table 'tbl_OrderType'
CREATE TABLE [dbo].[tbl_OrderType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsPhysicalDelivery] bit  NOT NULL,
    [ExpirationActionID] int  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [NumeratorID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ExpirationActionID] in table 'tbl_OrderType'
ALTER TABLE [dbo].[tbl_OrderType]
ADD CONSTRAINT [FK_tbl_OrderType_tbl_ExpirationAction]
    FOREIGN KEY ([ExpirationActionID])
    REFERENCES [dbo].[tbl_ExpirationAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [NumeratorID] in table 'tbl_OrderType'
ALTER TABLE [dbo].[tbl_OrderType]
ADD CONSTRAINT [FK_tbl_OrderType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_OrderType'
ALTER TABLE [dbo].[tbl_OrderType]
ADD CONSTRAINT [PK_tbl_OrderType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderType_tbl_ExpirationAction'
CREATE INDEX [IX_FK_tbl_OrderType_tbl_ExpirationAction]
ON [dbo].[tbl_OrderType]
    ([ExpirationActionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_OrderType_tbl_Numerator]
ON [dbo].[tbl_OrderType]
    ([NumeratorID]);