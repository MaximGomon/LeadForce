-- Creating table 'tbl_WidgetToAccessProfile'
CREATE TABLE [dbo].[tbl_WidgetToAccessProfile] (
    [ID] uniqueidentifier  NOT NULL,
    [WidgetID] uniqueidentifier  NOT NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [Order] int  NOT NULL,
    [ModuleID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [AccessProfileID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [WidgetID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_Widget]
    FOREIGN KEY ([WidgetID])
    REFERENCES [dbo].[tbl_Widget]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [PK_tbl_WidgetToAccessProfile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetToAccessProfile_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_WidgetToAccessProfile_tbl_AccessProfile]
ON [dbo].[tbl_WidgetToAccessProfile]
    ([AccessProfileID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetToAccessProfile_tbl_Module'
CREATE INDEX [IX_FK_tbl_WidgetToAccessProfile_tbl_Module]
ON [dbo].[tbl_WidgetToAccessProfile]
    ([ModuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetToAccessProfile_tbl_Widget'
CREATE INDEX [IX_FK_tbl_WidgetToAccessProfile_tbl_Widget]
ON [dbo].[tbl_WidgetToAccessProfile]
    ([WidgetID]);