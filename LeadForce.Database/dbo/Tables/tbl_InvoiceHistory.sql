-- Creating table 'tbl_InvoiceHistory'
CREATE TABLE [dbo].[tbl_InvoiceHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [InvoiceID] uniqueidentifier  NOT NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [PaymentDatePlanned] datetime  NULL,
    [PaymentDateActual] datetime  NULL,
    [InvoiceAmount] decimal(19,4)  NOT NULL,
    [InvoiceStatusID] int  NOT NULL,
    [IsExistBuyerComplaint] bit  NOT NULL,
    [Note] nvarchar(1024)  NULL
);
GO
-- Creating foreign key on [AuthorID] in table 'tbl_InvoiceHistory'
ALTER TABLE [dbo].[tbl_InvoiceHistory]
ADD CONSTRAINT [FK_tbl_InvoiceHistory_tbl_Contact]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceID] in table 'tbl_InvoiceHistory'
ALTER TABLE [dbo].[tbl_InvoiceHistory]
ADD CONSTRAINT [FK_tbl_InvoiceHistory_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceHistory'
ALTER TABLE [dbo].[tbl_InvoiceHistory]
ADD CONSTRAINT [PK_tbl_InvoiceHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_InvoiceHistory_tbl_Contact]
ON [dbo].[tbl_InvoiceHistory]
    ([AuthorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceHistory_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_InvoiceHistory_tbl_Invoice]
ON [dbo].[tbl_InvoiceHistory]
    ([InvoiceID]);