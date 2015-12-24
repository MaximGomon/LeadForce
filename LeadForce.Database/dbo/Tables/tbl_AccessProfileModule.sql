-- Creating table 'tbl_AccessProfileModule'
CREATE TABLE [dbo].[tbl_AccessProfileModule] (
    [ID] uniqueidentifier  NOT NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [Read] bit  NOT NULL,
    [Write] bit  NOT NULL,
    [Delete] bit  NOT NULL,
    [ModuleEditionID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [AccessProfileID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [FK_tbl_AccessProfileModule_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [FK_tbl_AccessProfileModule_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleEditionID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [FK_tbl_AccessProfileModule_tbl_ModuleEdition]
    FOREIGN KEY ([ModuleEditionID])
    REFERENCES [dbo].[tbl_ModuleEdition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [PK_tbl_AccessProfileModule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModule_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_AccessProfileModule_tbl_AccessProfile]
ON [dbo].[tbl_AccessProfileModule]
    ([AccessProfileID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModule_tbl_Module'
CREATE INDEX [IX_FK_tbl_AccessProfileModule_tbl_Module]
ON [dbo].[tbl_AccessProfileModule]
    ([ModuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModule_tbl_ModuleEdition'
CREATE INDEX [IX_FK_tbl_AccessProfileModule_tbl_ModuleEdition]
ON [dbo].[tbl_AccessProfileModule]
    ([ModuleEditionID]);