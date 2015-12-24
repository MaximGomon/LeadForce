-- Creating table 'tbl_InvoiceCommentMark'
CREATE TABLE [dbo].[tbl_InvoiceCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO
-- Creating foreign key on [ContentCommentID] in table 'tbl_InvoiceCommentMark'
ALTER TABLE [dbo].[tbl_InvoiceCommentMark]
ADD CONSTRAINT [FK_tbl_InvoiceCommentMark_tbl_InvoiceComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_InvoiceComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_InvoiceCommentMark'
ALTER TABLE [dbo].[tbl_InvoiceCommentMark]
ADD CONSTRAINT [FK_tbl_InvoiceCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceCommentMark'
ALTER TABLE [dbo].[tbl_InvoiceCommentMark]
ADD CONSTRAINT [PK_tbl_InvoiceCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceCommentMark_tbl_InvoiceComment'
CREATE INDEX [IX_FK_tbl_InvoiceCommentMark_tbl_InvoiceComment]
ON [dbo].[tbl_InvoiceCommentMark]
    ([ContentCommentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_InvoiceCommentMark_tbl_User]
ON [dbo].[tbl_InvoiceCommentMark]
    ([UserID]);