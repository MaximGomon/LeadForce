-- Creating table 'tbl_PaymentCFO'
CREATE TABLE [dbo].[tbl_PaymentCFO] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [PaymentPassCategoryID] uniqueidentifier  NOT NULL,
    [Note] nvarchar(max)  NOT NULL
);
GO
-- Creating foreign key on [PaymentPassCategoryID] in table 'tbl_PaymentCFO'
ALTER TABLE [dbo].[tbl_PaymentCFO]
ADD CONSTRAINT [FK_tbl_PaymentCFO_tbl_PaymentPassCategory]
    FOREIGN KEY ([PaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentCFO'
ALTER TABLE [dbo].[tbl_PaymentCFO]
ADD CONSTRAINT [FK_tbl_PaymentCFO_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentCFO'
ALTER TABLE [dbo].[tbl_PaymentCFO]
ADD CONSTRAINT [PK_tbl_PaymentCFO]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentCFO_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentCFO_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentCFO]
    ([PaymentPassCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentCFO_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentCFO_tbl_Sites]
ON [dbo].[tbl_PaymentCFO]
    ([SiteID]);