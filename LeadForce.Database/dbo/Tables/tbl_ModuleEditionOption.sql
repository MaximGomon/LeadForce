-- Creating table 'tbl_ModuleEditionOption'
CREATE TABLE [dbo].[tbl_ModuleEditionOption] (
    [ID] uniqueidentifier  NOT NULL,
    [ModuleEditionID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [SystemName] varchar(256)  NOT NULL,
    [ModuleEditionOptionType] int  NOT NULL
);
GO
-- Creating foreign key on [ModuleEditionID] in table 'tbl_ModuleEditionOption'
ALTER TABLE [dbo].[tbl_ModuleEditionOption]
ADD CONSTRAINT [FK_tbl_ModuleEditionOption_tbl_ModuleEdition]
    FOREIGN KEY ([ModuleEditionID])
    REFERENCES [dbo].[tbl_ModuleEdition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ModuleEditionOption'
ALTER TABLE [dbo].[tbl_ModuleEditionOption]
ADD CONSTRAINT [PK_tbl_ModuleEditionOption]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ModuleEditionOption_tbl_ModuleEdition'
CREATE INDEX [IX_FK_tbl_ModuleEditionOption_tbl_ModuleEdition]
ON [dbo].[tbl_ModuleEditionOption]
    ([ModuleEditionID]);