-- Creating table 'tbl_PaymentPassRule'
CREATE TABLE [dbo].[tbl_PaymentPassRule] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [PaymentTypeID] int  NULL,
    [IsActive] bit  NULL,
    [IsAutomatic] bit  NULL
);
GO
-- Creating foreign key on [PaymentTypeID] in table 'tbl_PaymentPassRule'
ALTER TABLE [dbo].[tbl_PaymentPassRule]
ADD CONSTRAINT [FK_tbl_PaymentPassRule_tbl_PaymentType]
    FOREIGN KEY ([PaymentTypeID])
    REFERENCES [dbo].[tbl_PaymentType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassRule'
ALTER TABLE [dbo].[tbl_PaymentPassRule]
ADD CONSTRAINT [FK_tbl_PaymentPassRule_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentPassRule'
ALTER TABLE [dbo].[tbl_PaymentPassRule]
ADD CONSTRAINT [PK_tbl_PaymentPassRule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRule_tbl_PaymentType'
CREATE INDEX [IX_FK_tbl_PaymentPassRule_tbl_PaymentType]
ON [dbo].[tbl_PaymentPassRule]
    ([PaymentTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRule_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPassRule_tbl_Sites]
ON [dbo].[tbl_PaymentPassRule]
    ([SiteID]);