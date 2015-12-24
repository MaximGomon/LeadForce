-- Creating table 'tbl_ProductComplectation'
CREATE TABLE [dbo].[tbl_ProductComplectation] (
    [ID] uniqueidentifier  NOT NULL,
    [BaseProductID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [Quantity] float  NOT NULL,
    [Price] decimal(19,4)  NOT NULL
);
GO
-- Creating foreign key on [BaseProductID] in table 'tbl_ProductComplectation'
ALTER TABLE [dbo].[tbl_ProductComplectation]
ADD CONSTRAINT [FK_tbl_ProductComplectation_tbl_Product]
    FOREIGN KEY ([BaseProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_ProductComplectation'
ALTER TABLE [dbo].[tbl_ProductComplectation]
ADD CONSTRAINT [FK_tbl_ProductComplectation_tbl_Product1]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ProductComplectation'
ALTER TABLE [dbo].[tbl_ProductComplectation]
ADD CONSTRAINT [PK_tbl_ProductComplectation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductComplectation_tbl_Product'
CREATE INDEX [IX_FK_tbl_ProductComplectation_tbl_Product]
ON [dbo].[tbl_ProductComplectation]
    ([BaseProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductComplectation_tbl_Product1'
CREATE INDEX [IX_FK_tbl_ProductComplectation_tbl_Product1]
ON [dbo].[tbl_ProductComplectation]
    ([ProductID]);