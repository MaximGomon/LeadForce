-- Creating table 'tbl_Widget'
CREATE TABLE [dbo].[tbl_Widget] (
    [ID] uniqueidentifier  NOT NULL,
    [WidgetCategoryID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [UserControl] nvarchar(2048)  NULL
);
GO
-- Creating foreign key on [WidgetCategoryID] in table 'tbl_Widget'
ALTER TABLE [dbo].[tbl_Widget]
ADD CONSTRAINT [FK_tbl_Widget_tbl_WidgetCategory]
    FOREIGN KEY ([WidgetCategoryID])
    REFERENCES [dbo].[tbl_WidgetCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Widget'
ALTER TABLE [dbo].[tbl_Widget]
ADD CONSTRAINT [PK_tbl_Widget]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Widget_tbl_WidgetCategory'
CREATE INDEX [IX_FK_tbl_Widget_tbl_WidgetCategory]
ON [dbo].[tbl_Widget]
    ([WidgetCategoryID]);