-- Creating table 'tbl_Region'
CREATE TABLE [dbo].[tbl_Region] (
    [ImportID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ID] uniqueidentifier  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [CountryID] in table 'tbl_Region'
ALTER TABLE [dbo].[tbl_Region]
ADD CONSTRAINT [FK_tbl_Region_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Region'
ALTER TABLE [dbo].[tbl_Region]
ADD CONSTRAINT [PK_tbl_Region]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Region_tbl_Country'
CREATE INDEX [IX_FK_tbl_Region_tbl_Country]
ON [dbo].[tbl_Region]
    ([CountryID]);