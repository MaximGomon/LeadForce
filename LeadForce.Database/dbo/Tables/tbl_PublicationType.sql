-- Creating table 'tbl_PublicationType'
CREATE TABLE [dbo].[tbl_PublicationType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [NumeratorID] uniqueidentifier  NULL,
    [Logo] nvarchar(250)  NULL,
    [TextAdd] nvarchar(250)  NOT NULL,
    [TextMarkToAdd] nvarchar(250)  NULL,
    [TextLike] nvarchar(250)  NOT NULL,
    [TextLikeComment] nvarchar(250)  NOT NULL,
    [PublicationKindID] int  NULL,
    [PublicationAccessRecordID] int  NULL,
    [PublicationAccessCommentID] int  NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [IsSearchable] bit  NOT NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [NumeratorID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationAccessCommentID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationAccessComment]
    FOREIGN KEY ([PublicationAccessCommentID])
    REFERENCES [dbo].[tbl_PublicationAccessComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationAccessRecordID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationAccessRecord]
    FOREIGN KEY ([PublicationAccessRecordID])
    REFERENCES [dbo].[tbl_PublicationAccessRecord]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationKindID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationKind]
    FOREIGN KEY ([PublicationKindID])
    REFERENCES [dbo].[tbl_PublicationKind]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [PK_tbl_PublicationType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_Numerator]
ON [dbo].[tbl_PublicationType]
    ([NumeratorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_PublicationAccessComment'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_PublicationAccessComment]
ON [dbo].[tbl_PublicationType]
    ([PublicationAccessCommentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_PublicationAccessRecord'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_PublicationAccessRecord]
ON [dbo].[tbl_PublicationType]
    ([PublicationAccessRecordID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_PublicationKind'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_PublicationKind]
ON [dbo].[tbl_PublicationType]
    ([PublicationKindID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_RequestSourceType]
ON [dbo].[tbl_PublicationType]
    ([RequestSourceTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_Sites]
ON [dbo].[tbl_PublicationType]
    ([SiteID]);