-- Creating table 'tbl_PublicationComment'
CREATE TABLE [dbo].[tbl_PublicationComment] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [FileName] nvarchar(250)  NOT NULL,
    [isOfficialAnswer] bit  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL
);
GO
-- Creating foreign key on [UserID] in table 'tbl_PublicationComment'
ALTER TABLE [dbo].[tbl_PublicationComment]
ADD CONSTRAINT [FK_tbl_PublicationComment_tbl_Contact]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationID] in table 'tbl_PublicationComment'
ALTER TABLE [dbo].[tbl_PublicationComment]
ADD CONSTRAINT [FK_tbl_PublicationComment_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PublicationComment'
ALTER TABLE [dbo].[tbl_PublicationComment]
ADD CONSTRAINT [PK_tbl_PublicationComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationComment_tbl_Contact'
CREATE INDEX [IX_FK_tbl_PublicationComment_tbl_Contact]
ON [dbo].[tbl_PublicationComment]
    ([UserID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationComment_tbl_Publication'
CREATE INDEX [IX_FK_tbl_PublicationComment_tbl_Publication]
ON [dbo].[tbl_PublicationComment]
    ([PublicationID]);