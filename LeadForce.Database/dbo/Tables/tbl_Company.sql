-- Creating table 'tbl_Company'
CREATE TABLE [dbo].[tbl_Company] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [CompanyTypeID] uniqueidentifier  NULL,
    [ParentID] uniqueidentifier  NULL,
    [CompanySizeID] uniqueidentifier  NULL,
    [CompanySectorID] uniqueidentifier  NULL,
    [Phone1] nvarchar(250)  NULL,
    [Phone2] nvarchar(250)  NULL,
    [Fax] nvarchar(250)  NULL,
    [Web] nvarchar(250)  NULL,
    [Email] nvarchar(250)  NULL,
    [EmailStatusID] int  NULL,
    [LocationAddressID] uniqueidentifier  NULL,
    [PostalAddressID] uniqueidentifier  NULL,
    [ReadyToSellID] uniqueidentifier  NULL,
    [PriorityID] uniqueidentifier  NULL,
    [StatusID] uniqueidentifier  NOT NULL,
    [Score] int  NOT NULL,
    [BehaviorScore] int  NOT NULL,
    [CharacteristicsScore] int  NOT NULL
);
GO
-- Creating foreign key on [LocationAddressID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_LocationAddress]
    FOREIGN KEY ([LocationAddressID])
    REFERENCES [dbo].[tbl_Address]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PostalAddressID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_PostalAddress]
    FOREIGN KEY ([PostalAddressID])
    REFERENCES [dbo].[tbl_Address]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CompanySectorID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_CompanySector]
    FOREIGN KEY ([CompanySectorID])
    REFERENCES [dbo].[tbl_CompanySector]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CompanySizeID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_CompanySize]
    FOREIGN KEY ([CompanySizeID])
    REFERENCES [dbo].[tbl_CompanySize]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CompanyTypeID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_CompanyType]
    FOREIGN KEY ([CompanyTypeID])
    REFERENCES [dbo].[tbl_CompanyType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OwnerID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriorityID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Priorities]
    FOREIGN KEY ([PriorityID])
    REFERENCES [dbo].[tbl_Priorities]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ReadyToSellID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_ReadyToSell]
    FOREIGN KEY ([ReadyToSellID])
    REFERENCES [dbo].[tbl_ReadyToSell]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [StatusID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Status]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[tbl_Status]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [PK_tbl_Company]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_LocationAddress'
CREATE INDEX [IX_FK_tbl_Company_LocationAddress]
ON [dbo].[tbl_Company]
    ([LocationAddressID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_PostalAddress'
CREATE INDEX [IX_FK_tbl_Company_PostalAddress]
ON [dbo].[tbl_Company]
    ([PostalAddressID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_CompanySector'
CREATE INDEX [IX_FK_tbl_Company_tbl_CompanySector]
ON [dbo].[tbl_Company]
    ([CompanySectorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_CompanySize'
CREATE INDEX [IX_FK_tbl_Company_tbl_CompanySize]
ON [dbo].[tbl_Company]
    ([CompanySizeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_CompanyType'
CREATE INDEX [IX_FK_tbl_Company_tbl_CompanyType]
ON [dbo].[tbl_Company]
    ([CompanyTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Company_tbl_Contact]
ON [dbo].[tbl_Company]
    ([OwnerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Priorities'
CREATE INDEX [IX_FK_tbl_Company_tbl_Priorities]
ON [dbo].[tbl_Company]
    ([PriorityID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_ReadyToSell'
CREATE INDEX [IX_FK_tbl_Company_tbl_ReadyToSell]
ON [dbo].[tbl_Company]
    ([ReadyToSellID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Company_tbl_Sites]
ON [dbo].[tbl_Company]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Status'
CREATE INDEX [IX_FK_tbl_Company_tbl_Status]
ON [dbo].[tbl_Company]
    ([StatusID]);