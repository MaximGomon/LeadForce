-- Creating table 'tbl_AccessProfileModuleEditionOption'
CREATE TABLE [dbo].[tbl_AccessProfileModuleEditionOption] (
    [tbl_AccessProfileModule_ID] uniqueidentifier  NOT NULL,
    [tbl_ModuleEditionOption_ID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [tbl_AccessProfileModule_ID] in table 'tbl_AccessProfileModuleEditionOption'
ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption]
ADD CONSTRAINT [FK_tbl_AccessProfileModuleEditionOption_tbl_AccessProfileModule]
    FOREIGN KEY ([tbl_AccessProfileModule_ID])
    REFERENCES [dbo].[tbl_AccessProfileModule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [tbl_ModuleEditionOption_ID] in table 'tbl_AccessProfileModuleEditionOption'
ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption]
ADD CONSTRAINT [FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption]
    FOREIGN KEY ([tbl_ModuleEditionOption_ID])
    REFERENCES [dbo].[tbl_ModuleEditionOption]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [tbl_AccessProfileModule_ID], [tbl_ModuleEditionOption_ID] in table 'tbl_AccessProfileModuleEditionOption'
ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption]
ADD CONSTRAINT [PK_tbl_AccessProfileModuleEditionOption]
    PRIMARY KEY CLUSTERED ([tbl_AccessProfileModule_ID], [tbl_ModuleEditionOption_ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption'
CREATE INDEX [IX_FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption]
ON [dbo].[tbl_AccessProfileModuleEditionOption]
    ([tbl_ModuleEditionOption_ID]);