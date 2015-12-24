-- Creating table 'tbl_Bank'
CREATE TABLE [dbo].[tbl_Bank] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [BIK] nvarchar(256)  NULL,
    [KS] nvarchar(256)  NULL,
    [CityID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [CityID] in table 'tbl_Bank'
ALTER TABLE [dbo].[tbl_Bank]
ADD CONSTRAINT [FK_tbl_Bank_tbl_City]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[tbl_City]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Bank'
ALTER TABLE [dbo].[tbl_Bank]
ADD CONSTRAINT [PK_tbl_Bank]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Bank_tbl_City'
CREATE INDEX [IX_FK_tbl_Bank_tbl_City]
ON [dbo].[tbl_Bank]
    ([CityID]);