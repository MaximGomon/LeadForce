-- Creating table 'tbl_ServiceLevelClient'
CREATE TABLE [dbo].[tbl_ServiceLevelClient] (
    [ID] uniqueidentifier  NOT NULL,
    [ServiceLevelID] uniqueidentifier  NOT NULL,
    [ClientID] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [CountOfServiceContacts] int  NOT NULL,
    [OutOfListServiceContactsID] int  NULL
);
GO
-- Creating foreign key on [ClientID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_Company]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ServiceLevelID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_ServiceLevel]
    FOREIGN KEY ([ServiceLevelID])
    REFERENCES [dbo].[tbl_ServiceLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OutOfListServiceContactsID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts]
    FOREIGN KEY ([OutOfListServiceContactsID])
    REFERENCES [dbo].[tbl_ServiceLevelOutOfListServiceContacts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [PK_tbl_ServiceLevelClient]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelClient_tbl_Company'
CREATE INDEX [IX_FK_tbl_ServiceLevelClient_tbl_Company]
ON [dbo].[tbl_ServiceLevelClient]
    ([ClientID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelClient_tbl_ServiceLevel'
CREATE INDEX [IX_FK_tbl_ServiceLevelClient_tbl_ServiceLevel]
ON [dbo].[tbl_ServiceLevelClient]
    ([ServiceLevelID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts'
CREATE INDEX [IX_FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts]
ON [dbo].[tbl_ServiceLevelClient]
    ([OutOfListServiceContactsID]);