-- Creating table 'tbl_ModuleEditionAction'
CREATE TABLE [dbo].[tbl_ModuleEditionAction] (
    [ID] uniqueidentifier  NOT NULL,
    [ModuleEditionID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [SystemName] varchar(256)  NOT NULL,
    [IconPath] nvarchar(1024)  NULL,
    [UserControl] nvarchar(1024)  NOT NULL
);
GO
-- Creating foreign key on [ModuleEditionID] in table 'tbl_ModuleEditionAction'
ALTER TABLE [dbo].[tbl_ModuleEditionAction]
ADD CONSTRAINT [FK_tbl_ModuleEditionAction_tbl_ModuleEdition]
    FOREIGN KEY ([ModuleEditionID])
    REFERENCES [dbo].[tbl_ModuleEdition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ModuleEditionAction'
ALTER TABLE [dbo].[tbl_ModuleEditionAction]
ADD CONSTRAINT [PK_tbl_ModuleEditionAction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ModuleEditionAction_tbl_ModuleEdition'
CREATE INDEX [IX_FK_tbl_ModuleEditionAction_tbl_ModuleEdition]
ON [dbo].[tbl_ModuleEditionAction]
    ([ModuleEditionID]);