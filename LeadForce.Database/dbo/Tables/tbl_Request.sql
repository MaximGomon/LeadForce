-- Creating table 'tbl_Request'
CREATE TABLE [dbo].[tbl_Request] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL,
    [RequestSourceID] uniqueidentifier  NULL,
    [CompanyID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [ProductID] uniqueidentifier  NULL,
    [ProductSeriesNumber] nvarchar(256)  NULL,
    [RequestStatusID] int  NOT NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [ServiceLevelID] uniqueidentifier  NULL,
    [ReactionDatePlanned] datetime  NULL,
    [ReactionDateActual] datetime  NULL,
    [LongDescription] nvarchar(max)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [ShortDescription] nvarchar(2048)  NULL
);
GO
-- Creating foreign key on [CompanyID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OwnerID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Contact_Owner]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Contact_Responsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestStatusID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_RequestStatus]
    FOREIGN KEY ([RequestStatusID])
    REFERENCES [dbo].[tbl_RequestStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ServiceLevelID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_ServiceLevel]
    FOREIGN KEY ([ServiceLevelID])
    REFERENCES [dbo].[tbl_ServiceLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [PK_tbl_Request]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Company'
CREATE INDEX [IX_FK_tbl_Request_tbl_Company]
ON [dbo].[tbl_Request]
    ([CompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Request_tbl_Contact]
ON [dbo].[tbl_Request]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Contact_Owner'
CREATE INDEX [IX_FK_tbl_Request_tbl_Contact_Owner]
ON [dbo].[tbl_Request]
    ([OwnerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Contact_Responsible'
CREATE INDEX [IX_FK_tbl_Request_tbl_Contact_Responsible]
ON [dbo].[tbl_Request]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Product'
CREATE INDEX [IX_FK_tbl_Request_tbl_Product]
ON [dbo].[tbl_Request]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_Request_tbl_RequestSourceType]
ON [dbo].[tbl_Request]
    ([RequestSourceTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_RequestStatus'
CREATE INDEX [IX_FK_tbl_Request_tbl_RequestStatus]
ON [dbo].[tbl_Request]
    ([RequestStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_ServiceLevel'
CREATE INDEX [IX_FK_tbl_Request_tbl_ServiceLevel]
ON [dbo].[tbl_Request]
    ([ServiceLevelID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Request_tbl_Sites]
ON [dbo].[tbl_Request]
    ([SiteID]);