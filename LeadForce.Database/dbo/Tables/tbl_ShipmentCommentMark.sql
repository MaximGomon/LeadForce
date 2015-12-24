-- Creating table 'tbl_ShipmentCommentMark'
CREATE TABLE [dbo].[tbl_ShipmentCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO
-- Creating foreign key on [ContentCommentID] in table 'tbl_ShipmentCommentMark'
ALTER TABLE [dbo].[tbl_ShipmentCommentMark]
ADD CONSTRAINT [FK_tbl_ShipmentCommentMark_tbl_ShipmentComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_ShipmentComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_ShipmentCommentMark'
ALTER TABLE [dbo].[tbl_ShipmentCommentMark]
ADD CONSTRAINT [FK_tbl_ShipmentCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ShipmentCommentMark'
ALTER TABLE [dbo].[tbl_ShipmentCommentMark]
ADD CONSTRAINT [PK_tbl_ShipmentCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentCommentMark_tbl_ShipmentComment'
CREATE INDEX [IX_FK_tbl_ShipmentCommentMark_tbl_ShipmentComment]
ON [dbo].[tbl_ShipmentCommentMark]
    ([ContentCommentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_ShipmentCommentMark_tbl_User]
ON [dbo].[tbl_ShipmentCommentMark]
    ([UserID]);