-- Creating table 'tbl_InvoiceType'
CREATE TABLE [dbo].[tbl_InvoiceType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [NumeratorID] uniqueidentifier  NULL,
    [DirectionID] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [DirectionID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [FK_tbl_InvoiceType_tbl_Direction]
    FOREIGN KEY ([DirectionID])
    REFERENCES [dbo].[tbl_Direction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [NumeratorID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [FK_tbl_InvoiceType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [FK_tbl_InvoiceType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [PK_tbl_InvoiceType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceType_tbl_Direction'
CREATE INDEX [IX_FK_tbl_InvoiceType_tbl_Direction]
ON [dbo].[tbl_InvoiceType]
    ([DirectionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_InvoiceType_tbl_Numerator]
ON [dbo].[tbl_InvoiceType]
    ([NumeratorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_InvoiceType_tbl_Sites]
ON [dbo].[tbl_InvoiceType]
    ([SiteID]);