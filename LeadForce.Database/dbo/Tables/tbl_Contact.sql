-- Creating table 'tbl_Contact'
CREATE TABLE [dbo].[tbl_Contact] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [LastActivityAt] datetime  NULL,
    [RefferURL] nvarchar(2000)  NOT NULL,
    [UserIP] varchar(15)  NOT NULL,
    [UserFullName] nvarchar(255)  NULL,
    [Email] nvarchar(255)  NULL,
    [Phone] varchar(50)  NULL,
    [ReadyToSellID] uniqueidentifier  NULL,
    [PriorityID] uniqueidentifier  NULL,
    [StatusID] uniqueidentifier  NOT NULL,
    [Score] int  NOT NULL,
    [BehaviorScore] int  NOT NULL,
    [CharacteristicsScore] int  NOT NULL,
    [IsNameChecked] bit  NOT NULL,
    [Surname] nvarchar(255)  NULL,
    [Name] nvarchar(255)  NULL,
    [Patronymic] nvarchar(255)  NULL,
    [CellularPhone] varchar(50)  NULL,
    [CellularPhoneStatusID] int  NULL,
    [EmailStatusID] int  NULL,
    [ContactTypeID] uniqueidentifier  NULL,
    [JobTitle] nvarchar(250)  NULL,
    [CompanyID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [BirthDate] datetime  NULL,
    [ContactFunctionInCompanyID] uniqueidentifier  NULL,
    [ContactJobLevelID] uniqueidentifier  NULL,
    [AddressID] uniqueidentifier  NULL,
    [RefferID] uniqueidentifier  NULL,
    [AdvertisingTypeID] uniqueidentifier  NULL,
    [AdvertisingPlatformID] uniqueidentifier  NULL,
    [AdvertisingCampaignID] uniqueidentifier  NULL,
    [Gender] int  NULL,
    [Category] int  NOT NULL,
    [RegistrationSourceID] int  NOT NULL,
    [Comment] nvarchar(2048)  NULL,
    [CameFromURL] nvarchar(2000)  NULL
);
GO
-- Creating foreign key on [AddressID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Address]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[tbl_Address]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AdvertisingCampaignID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingCampaign]
    FOREIGN KEY ([AdvertisingCampaignID])
    REFERENCES [dbo].[tbl_AdvertisingCampaign]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AdvertisingPlatformID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingPlatform]
    FOREIGN KEY ([AdvertisingPlatformID])
    REFERENCES [dbo].[tbl_AdvertisingPlatform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AdvertisingTypeID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingType]
    FOREIGN KEY ([AdvertisingTypeID])
    REFERENCES [dbo].[tbl_AdvertisingType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CompanyID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OwnerID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactFunctionInCompanyID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ContactFunctionInCompany]
    FOREIGN KEY ([ContactFunctionInCompanyID])
    REFERENCES [dbo].[tbl_ContactFunctionInCompany]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactJobLevelID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ContactJobLevel]
    FOREIGN KEY ([ContactJobLevelID])
    REFERENCES [dbo].[tbl_ContactJobLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactTypeID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ContactType]
    FOREIGN KEY ([ContactTypeID])
    REFERENCES [dbo].[tbl_ContactType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriorityID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Priorities]
    FOREIGN KEY ([PriorityID])
    REFERENCES [dbo].[tbl_Priorities]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ReadyToSellID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ReadyToSell]
    FOREIGN KEY ([ReadyToSellID])
    REFERENCES [dbo].[tbl_ReadyToSell]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [StatusID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Status]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[tbl_Status]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [PK_tbl_Contact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Address'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Address]
ON [dbo].[tbl_Contact]
    ([AddressID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_AdvertisingCampaign'
CREATE INDEX [IX_FK_tbl_Contact_tbl_AdvertisingCampaign]
ON [dbo].[tbl_Contact]
    ([AdvertisingCampaignID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_AdvertisingPlatform'
CREATE INDEX [IX_FK_tbl_Contact_tbl_AdvertisingPlatform]
ON [dbo].[tbl_Contact]
    ([AdvertisingPlatformID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_AdvertisingType'
CREATE INDEX [IX_FK_tbl_Contact_tbl_AdvertisingType]
ON [dbo].[tbl_Contact]
    ([AdvertisingTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Company'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Company]
ON [dbo].[tbl_Contact]
    ([CompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Contact]
ON [dbo].[tbl_Contact]
    ([OwnerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ContactFunctionInCompany'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ContactFunctionInCompany]
ON [dbo].[tbl_Contact]
    ([ContactFunctionInCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ContactJobLevel'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ContactJobLevel]
ON [dbo].[tbl_Contact]
    ([ContactJobLevelID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ContactType'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ContactType]
ON [dbo].[tbl_Contact]
    ([ContactTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Priorities'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Priorities]
ON [dbo].[tbl_Contact]
    ([PriorityID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ReadyToSell'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ReadyToSell]
ON [dbo].[tbl_Contact]
    ([ReadyToSellID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Sites]
ON [dbo].[tbl_Contact]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Status'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Status]
ON [dbo].[tbl_Contact]
    ([StatusID]);