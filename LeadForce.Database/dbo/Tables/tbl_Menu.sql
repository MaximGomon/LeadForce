-- Creating table 'tbl_Menu'
CREATE TABLE [dbo].[tbl_Menu] (
    [ID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NULL,
    [TabName] nvarchar(255)  NULL,
    [ModuleID] uniqueidentifier  NULL,
    [Order] int  NOT NULL,
    [ModuleEditionActionID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [AccessProfileID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [FK_tbl_Menu_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [FK_tbl_Menu_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleEditionActionID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [FK_tbl_Menu_tbl_ModuleEditionAction]
    FOREIGN KEY ([ModuleEditionActionID])
    REFERENCES [dbo].[tbl_ModuleEditionAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [PK_tbl_Menu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Menu_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_Menu_tbl_AccessProfile]
ON [dbo].[tbl_Menu]
    ([AccessProfileID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Menu_tbl_Module'
CREATE INDEX [IX_FK_tbl_Menu_tbl_Module]
ON [dbo].[tbl_Menu]
    ([ModuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Menu_tbl_ModuleEditionAction'
CREATE INDEX [IX_FK_tbl_Menu_tbl_ModuleEditionAction]
ON [dbo].[tbl_Menu]
    ([ModuleEditionActionID]);