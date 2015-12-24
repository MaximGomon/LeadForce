-- Creating table 'tbl_RequirementCommentMark'
CREATE TABLE [dbo].[tbl_RequirementCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO
-- Creating foreign key on [ContentCommentID] in table 'tbl_RequirementCommentMark'
ALTER TABLE [dbo].[tbl_RequirementCommentMark]
ADD CONSTRAINT [FK_tbl_RequirementCommentMark_tbl_RequirementComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_RequirementComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_RequirementCommentMark'
ALTER TABLE [dbo].[tbl_RequirementCommentMark]
ADD CONSTRAINT [FK_tbl_RequirementCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementCommentMark'
ALTER TABLE [dbo].[tbl_RequirementCommentMark]
ADD CONSTRAINT [PK_tbl_RequirementCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementCommentMark_tbl_RequirementComment'
CREATE INDEX [IX_FK_tbl_RequirementCommentMark_tbl_RequirementComment]
ON [dbo].[tbl_RequirementCommentMark]
    ([ContentCommentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_RequirementCommentMark_tbl_User]
ON [dbo].[tbl_RequirementCommentMark]
    ([UserID]);