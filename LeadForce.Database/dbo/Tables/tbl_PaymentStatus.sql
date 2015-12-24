-- Creating table 'tbl_PaymentStatus'
CREATE TABLE [dbo].[tbl_PaymentStatus] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [IsLast] bit  NOT NULL
);
GO
-- Creating foreign key on [ID] in table 'tbl_PaymentStatus'
ALTER TABLE [dbo].[tbl_PaymentStatus]
ADD CONSTRAINT [FK_tbl_PaymentStatus_tbl_PaymentStatus]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentStatus'
ALTER TABLE [dbo].[tbl_PaymentStatus]
ADD CONSTRAINT [FK_tbl_PaymentStatus_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentStatus'
ALTER TABLE [dbo].[tbl_PaymentStatus]
ADD CONSTRAINT [PK_tbl_PaymentStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentStatus_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentStatus_tbl_Sites]
ON [dbo].[tbl_PaymentStatus]
    ([SiteID]);