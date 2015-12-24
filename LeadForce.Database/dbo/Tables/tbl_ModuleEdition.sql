-- Creating table 'tbl_ModuleEdition'
CREATE TABLE [dbo].[tbl_ModuleEdition] (
    [ID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [ModuleID] in table 'tbl_ModuleEdition'
ALTER TABLE [dbo].[tbl_ModuleEdition]
ADD CONSTRAINT [FK_tbl_ModuleEdition_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ModuleEdition'
ALTER TABLE [dbo].[tbl_ModuleEdition]
ADD CONSTRAINT [PK_tbl_ModuleEdition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ModuleEdition_tbl_Module'
CREATE INDEX [IX_FK_tbl_ModuleEdition_tbl_Module]
ON [dbo].[tbl_ModuleEdition]
    ([ModuleID]);