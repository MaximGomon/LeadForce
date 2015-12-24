-- Creating table 'tbl_PublicationStatusToPublicationType'
CREATE TABLE [dbo].[tbl_PublicationStatusToPublicationType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PublicationStatusID] uniqueidentifier  NOT NULL,
    [PublicationTypeID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [PublicationStatusID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus]
    FOREIGN KEY ([PublicationStatusID])
    REFERENCES [dbo].[tbl_PublicationStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationTypeID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType]
    FOREIGN KEY ([PublicationTypeID])
    REFERENCES [dbo].[tbl_PublicationType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [PK_tbl_PublicationStatusToPublicationType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus'
CREATE INDEX [IX_FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus]
ON [dbo].[tbl_PublicationStatusToPublicationType]
    ([PublicationStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType'
CREATE INDEX [IX_FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType]
ON [dbo].[tbl_PublicationStatusToPublicationType]
    ([PublicationTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationStatusToPublicationType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PublicationStatusToPublicationType_tbl_Sites]
ON [dbo].[tbl_PublicationStatusToPublicationType]
    ([SiteID]);