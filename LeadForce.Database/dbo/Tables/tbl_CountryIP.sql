-- Creating table 'tbl_CountryIP'
CREATE TABLE [dbo].[tbl_CountryIP] (
    [ImportCountryID] int  NOT NULL,
    [BeginIP] bigint  NOT NULL,
    [EndIP] bigint  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [CountryID] in table 'tbl_CountryIP'
ALTER TABLE [dbo].[tbl_CountryIP]
ADD CONSTRAINT [FK_tbl_CountryIP_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ImportCountryID], [BeginIP], [EndIP], [CountryID] in table 'tbl_CountryIP'
ALTER TABLE [dbo].[tbl_CountryIP]
ADD CONSTRAINT [PK_tbl_CountryIP]
    PRIMARY KEY CLUSTERED ([ImportCountryID], [BeginIP], [EndIP], [CountryID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CountryIP_tbl_Country'
CREATE INDEX [IX_FK_tbl_CountryIP_tbl_Country]
ON [dbo].[tbl_CountryIP]
    ([CountryID]);