-- Creating table 'tbl_PaymentPassRuleCompany'
CREATE TABLE [dbo].[tbl_PaymentPassRuleCompany] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentPassRuleID] uniqueidentifier  NOT NULL,
    [PayerID] uniqueidentifier  NULL,
    [PayerLegalAccountID] uniqueidentifier  NULL,
    [RecipientID] uniqueidentifier  NULL,
    [RecipientLegalAccountID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [PayerID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Company]
    FOREIGN KEY ([PayerID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RecipientID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Company1]
    FOREIGN KEY ([RecipientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PayerLegalAccountID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount]
    FOREIGN KEY ([PayerLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RecipientLegalAccountID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1]
    FOREIGN KEY ([RecipientLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PaymentPassRuleID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule]
    FOREIGN KEY ([PaymentPassRuleID])
    REFERENCES [dbo].[tbl_PaymentPassRule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [PK_tbl_PaymentPassRuleCompany]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_Company'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_Company]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([PayerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_Company1'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_Company1]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([RecipientID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([PayerLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([RecipientLegalAccountID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([PaymentPassRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_Sites]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([SiteID]);