-- Creating table 'tbl_PaymentPassCategory'
CREATE TABLE [dbo].[tbl_PaymentPassCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Note] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassCategory'
ALTER TABLE [dbo].[tbl_PaymentPassCategory]
ADD CONSTRAINT [FK_tbl_PaymentPassCategory_tbl_Sites1]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentPassCategory'
ALTER TABLE [dbo].[tbl_PaymentPassCategory]
ADD CONSTRAINT [PK_tbl_PaymentPassCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassCategory_tbl_Sites1'
CREATE INDEX [IX_FK_tbl_PaymentPassCategory_tbl_Sites1]
ON [dbo].[tbl_PaymentPassCategory]
    ([SiteID]);