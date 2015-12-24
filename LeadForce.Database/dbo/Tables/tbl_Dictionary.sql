-- Creating table 'tbl_Dictionary'
CREATE TABLE [dbo].[tbl_Dictionary] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(128)  NOT NULL,
    [DataSet] nvarchar(50)  NOT NULL,
    [AccessLevelID] int  NOT NULL,
    [DictionaryGroupID] uniqueidentifier  NULL,
    [EditFormUserControl] nvarchar(1024)  NULL
);
GO
-- Creating foreign key on [DictionaryGroupID] in table 'tbl_Dictionary'
ALTER TABLE [dbo].[tbl_Dictionary]
ADD CONSTRAINT [FK_tbl_Dictionary_tbl_DictionaryGroup]
    FOREIGN KEY ([DictionaryGroupID])
    REFERENCES [dbo].[tbl_DictionaryGroup]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Dictionary'
ALTER TABLE [dbo].[tbl_Dictionary]
ADD CONSTRAINT [PK_tbl_Dictionary]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Dictionary_tbl_DictionaryGroup'
CREATE INDEX [IX_FK_tbl_Dictionary_tbl_DictionaryGroup]
ON [dbo].[tbl_Dictionary]
    ([DictionaryGroupID]);