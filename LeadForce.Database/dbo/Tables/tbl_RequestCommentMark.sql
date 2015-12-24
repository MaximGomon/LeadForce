-- Creating table 'tbl_RequestCommentMark'
CREATE TABLE [dbo].[tbl_RequestCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO
-- Creating foreign key on [ContentCommentID] in table 'tbl_RequestCommentMark'
ALTER TABLE [dbo].[tbl_RequestCommentMark]
ADD CONSTRAINT [FK_tbl_RequestCommentMark_tbl_RequestComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_RequestComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_RequestCommentMark'
ALTER TABLE [dbo].[tbl_RequestCommentMark]
ADD CONSTRAINT [FK_tbl_RequestCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequestCommentMark'
ALTER TABLE [dbo].[tbl_RequestCommentMark]
ADD CONSTRAINT [PK_tbl_RequestCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestCommentMark_tbl_RequestComment'
CREATE INDEX [IX_FK_tbl_RequestCommentMark_tbl_RequestComment]
ON [dbo].[tbl_RequestCommentMark]
    ([ContentCommentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_RequestCommentMark_tbl_User]
ON [dbo].[tbl_RequestCommentMark]
    ([UserID]);