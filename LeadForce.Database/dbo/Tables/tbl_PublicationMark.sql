-- Creating table 'tbl_PublicationMark'
CREATE TABLE [dbo].[tbl_PublicationMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [PublicationCommentID] uniqueidentifier  NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO
-- Creating foreign key on [UserID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [FK_tbl_PublicationMark_tbl_Contact]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [FK_tbl_PublicationMark_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationCommentID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [FK_tbl_PublicationMark_tbl_PublicationComment]
    FOREIGN KEY ([PublicationCommentID])
    REFERENCES [dbo].[tbl_PublicationComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [PK_tbl_PublicationMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationMark_tbl_Contact'
CREATE INDEX [IX_FK_tbl_PublicationMark_tbl_Contact]
ON [dbo].[tbl_PublicationMark]
    ([UserID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationMark_tbl_Publication'
CREATE INDEX [IX_FK_tbl_PublicationMark_tbl_Publication]
ON [dbo].[tbl_PublicationMark]
    ([PublicationID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationMark_tbl_PublicationComment'
CREATE INDEX [IX_FK_tbl_PublicationMark_tbl_PublicationComment]
ON [dbo].[tbl_PublicationMark]
    ([PublicationCommentID]);