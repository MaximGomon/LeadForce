-- Creating table 'tbl_DictionaryGroup'
CREATE TABLE [dbo].[tbl_DictionaryGroup] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ModuleID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ModuleID] in table 'tbl_DictionaryGroup'
ALTER TABLE [dbo].[tbl_DictionaryGroup]
ADD CONSTRAINT [FK_tbl_DictionaryGroup_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_DictionaryGroup'
ALTER TABLE [dbo].[tbl_DictionaryGroup]
ADD CONSTRAINT [PK_tbl_DictionaryGroup]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_DictionaryGroup_tbl_Module'
CREATE INDEX [IX_FK_tbl_DictionaryGroup_tbl_Module]
ON [dbo].[tbl_DictionaryGroup]
    ([ModuleID]);