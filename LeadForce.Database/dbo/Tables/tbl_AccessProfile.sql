-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_AccessProfile'
CREATE TABLE [dbo].[tbl_AccessProfile] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NULL,
    [Title] nvarchar(255)  NOT NULL,
    [DomainsCount] int  NULL,
    [ContactsPageUrl] nvarchar(2048)  NULL,
    [ProductID] uniqueidentifier  NULL,
    [ActiveContactsCount] int  NULL,
    [EmailPerContactCount] int  NULL,
    [DurationPeriod] int  NULL
);
GO
-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductID] in table 'tbl_AccessProfile'
ALTER TABLE [dbo].[tbl_AccessProfile]
ADD CONSTRAINT [FK_tbl_AccessProfile_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_AccessProfile'
ALTER TABLE [dbo].[tbl_AccessProfile]
ADD CONSTRAINT [FK_tbl_AccessProfile_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'tbl_AccessProfile'
ALTER TABLE [dbo].[tbl_AccessProfile]
ADD CONSTRAINT [PK_tbl_AccessProfile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfile_tbl_Product'
CREATE INDEX [IX_FK_tbl_AccessProfile_tbl_Product]
ON [dbo].[tbl_AccessProfile]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfile_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AccessProfile_tbl_Sites]
ON [dbo].[tbl_AccessProfile]
    ([SiteID]);