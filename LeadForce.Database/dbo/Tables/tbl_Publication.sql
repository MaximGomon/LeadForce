-- Creating table 'tbl_Publication'
CREATE TABLE [dbo].[tbl_Publication] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Code] nvarchar(250)  NULL,
    [Date] datetime  NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [PublicationStatusID] uniqueidentifier  NULL,
    [PublicationTypeID] uniqueidentifier  NOT NULL,
    [PublicationCategoryID] uniqueidentifier  NOT NULL,
    [Img] varbinary(max)  NULL,
    [FileName] nvarchar(250)  NULL,
    [Noun] nvarchar(max)  NULL,
    [Text] nvarchar(max)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [AccessRecord] int  NULL,
    [AccessComment] int  NULL,
    [AccessCompanyID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [AccessCompanyID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_Company]
    FOREIGN KEY ([AccessCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AuthorID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_Contact]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationCategoryID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_PublicationCategory]
    FOREIGN KEY ([PublicationCategoryID])
    REFERENCES [dbo].[tbl_PublicationCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationStatusID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_PublicationStatus]
    FOREIGN KEY ([PublicationStatusID])
    REFERENCES [dbo].[tbl_PublicationStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationTypeID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_PublicationType]
    FOREIGN KEY ([PublicationTypeID])
    REFERENCES [dbo].[tbl_PublicationType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [PK_tbl_Publication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_Company'
CREATE INDEX [IX_FK_tbl_Publication_tbl_Company]
ON [dbo].[tbl_Publication]
    ([AccessCompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Publication_tbl_Contact]
ON [dbo].[tbl_Publication]
    ([AuthorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_PublicationCategory'
CREATE INDEX [IX_FK_tbl_Publication_tbl_PublicationCategory]
ON [dbo].[tbl_Publication]
    ([PublicationCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_PublicationStatus'
CREATE INDEX [IX_FK_tbl_Publication_tbl_PublicationStatus]
ON [dbo].[tbl_Publication]
    ([PublicationStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_PublicationType'
CREATE INDEX [IX_FK_tbl_Publication_tbl_PublicationType]
ON [dbo].[tbl_Publication]
    ([PublicationTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Publication_tbl_Sites]
ON [dbo].[tbl_Publication]
    ([SiteID]);