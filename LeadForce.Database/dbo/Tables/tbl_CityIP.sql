-- Creating table 'tbl_CityIP'
CREATE TABLE [dbo].[tbl_CityIP] (
    [ImportCityID] int  NOT NULL,
    [BeginIP] bigint  NOT NULL,
    [EndIP] bigint  NOT NULL,
    [CityID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [CityID] in table 'tbl_CityIP'
ALTER TABLE [dbo].[tbl_CityIP]
ADD CONSTRAINT [FK_tbl_CityIP_tbl_City]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[tbl_City]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ImportCityID], [BeginIP], [EndIP], [CityID] in table 'tbl_CityIP'
ALTER TABLE [dbo].[tbl_CityIP]
ADD CONSTRAINT [PK_tbl_CityIP]
    PRIMARY KEY CLUSTERED ([ImportCityID], [BeginIP], [EndIP], [CityID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CityIP_tbl_City'
CREATE INDEX [IX_FK_tbl_CityIP_tbl_City]
ON [dbo].[tbl_CityIP]
    ([CityID]);