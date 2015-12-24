-- Creating table 'tbl_PaymentTransition'
CREATE TABLE [dbo].[tbl_PaymentTransition] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [InitialPaymentStatusID] uniqueidentifier  NOT NULL,
    [FinalPaymentStatusID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [InitialPaymentStatusID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [FK_tbl_PaymentTransition_tbl_PaymentStatus]
    FOREIGN KEY ([InitialPaymentStatusID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [FinalPaymentStatusID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [FK_tbl_PaymentTransition_tbl_PaymentStatus1]
    FOREIGN KEY ([FinalPaymentStatusID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [FK_tbl_PaymentTransition_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [PK_tbl_PaymentTransition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentTransition_tbl_PaymentStatus'
CREATE INDEX [IX_FK_tbl_PaymentTransition_tbl_PaymentStatus]
ON [dbo].[tbl_PaymentTransition]
    ([InitialPaymentStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentTransition_tbl_PaymentStatus1'
CREATE INDEX [IX_FK_tbl_PaymentTransition_tbl_PaymentStatus1]
ON [dbo].[tbl_PaymentTransition]
    ([FinalPaymentStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentTransition_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentTransition_tbl_Sites]
ON [dbo].[tbl_PaymentTransition]
    ([SiteID]);