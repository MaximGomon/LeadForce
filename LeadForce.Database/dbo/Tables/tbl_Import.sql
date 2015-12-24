-- Creating table 'tbl_Import'
CREATE TABLE [dbo].[tbl_Import] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ImportTable] tinyint  NOT NULL,
    [SheetName] nvarchar(255)  NOT NULL,
    [FirstRow] int  NOT NULL,
    [FirstColumn] int  NOT NULL,
    [IsFirstRowAsColumnNames] bit  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [Type] int  NOT NULL,
    [CsvSeparator] nvarchar(10)  NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_Import'
ALTER TABLE [dbo].[tbl_Import]
ADD CONSTRAINT [FK_tbl_Import_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Import'
ALTER TABLE [dbo].[tbl_Import]
ADD CONSTRAINT [PK_tbl_Import]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Import_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Import_tbl_Sites]
ON [dbo].[tbl_Import]
    ([SiteID]);