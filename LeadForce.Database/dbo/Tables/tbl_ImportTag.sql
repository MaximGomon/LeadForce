-- Creating table 'tbl_ImportTag'
CREATE TABLE [dbo].[tbl_ImportTag] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [SiteTagID] uniqueidentifier  NOT NULL,
    [Operation] int  NOT NULL
);
GO
-- Creating foreign key on [ImportID] in table 'tbl_ImportTag'
ALTER TABLE [dbo].[tbl_ImportTag]
ADD CONSTRAINT [FK_tbl_ImportTag_tbl_Import]
    FOREIGN KEY ([ImportID])
    REFERENCES [dbo].[tbl_Import]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ImportTag'
ALTER TABLE [dbo].[tbl_ImportTag]
ADD CONSTRAINT [PK_tbl_ImportTag]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ImportTag_tbl_Import'
CREATE INDEX [IX_FK_tbl_ImportTag_tbl_Import]
ON [dbo].[tbl_ImportTag]
    ([ImportID]);