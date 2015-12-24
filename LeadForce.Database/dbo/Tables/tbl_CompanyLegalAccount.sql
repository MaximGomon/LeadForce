-- Creating table 'tbl_CompanyLegalAccount'
CREATE TABLE [dbo].[tbl_CompanyLegalAccount] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [CompanyID] uniqueidentifier  NOT NULL,
    [OfficialTitle] nvarchar(256)  NULL,
    [LegalAddress] nvarchar(2048)  NULL,
    [OGRN] nvarchar(256)  NULL,
    [RegistrationDate] datetime  NULL,
    [INN] nvarchar(256)  NULL,
    [KPP] nvarchar(256)  NULL,
    [RS] nvarchar(256)  NULL,
    [BankID] uniqueidentifier  NULL,
    [IsPrimary] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [HeadSignatureFileName] nvarchar(512)  NULL,
    [AccountantID] uniqueidentifier  NULL,
    [AccountantSignatureFileName] nvarchar(512)  NULL,
    [HeadID] uniqueidentifier  NULL,
    [StampFileName] nvarchar(512)  NULL
);
GO
-- Creating foreign key on [BankID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Bank]
    FOREIGN KEY ([BankID])
    REFERENCES [dbo].[tbl_Bank]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CompanyID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AccountantID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant]
    FOREIGN KEY ([AccountantID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [HeadID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Contact_Head]
    FOREIGN KEY ([HeadID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [PK_tbl_CompanyLegalAccount]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Bank'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Bank]
ON [dbo].[tbl_CompanyLegalAccount]
    ([BankID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Company'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Company]
ON [dbo].[tbl_CompanyLegalAccount]
    ([CompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant]
ON [dbo].[tbl_CompanyLegalAccount]
    ([AccountantID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Contact_Head'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Contact_Head]
ON [dbo].[tbl_CompanyLegalAccount]
    ([HeadID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Sites]
ON [dbo].[tbl_CompanyLegalAccount]
    ([SiteID]);