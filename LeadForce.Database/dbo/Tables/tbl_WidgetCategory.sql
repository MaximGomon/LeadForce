-- Creating table 'tbl_WidgetCategory'
CREATE TABLE [dbo].[tbl_WidgetCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [ParentID] in table 'tbl_WidgetCategory'
ALTER TABLE [dbo].[tbl_WidgetCategory]
ADD CONSTRAINT [FK_tbl_WidgetCategory_tbl_WidgetCategory]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_WidgetCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WidgetCategory'
ALTER TABLE [dbo].[tbl_WidgetCategory]
ADD CONSTRAINT [PK_tbl_WidgetCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetCategory_tbl_WidgetCategory'
CREATE INDEX [IX_FK_tbl_WidgetCategory_tbl_WidgetCategory]
ON [dbo].[tbl_WidgetCategory]
    ([ParentID]);