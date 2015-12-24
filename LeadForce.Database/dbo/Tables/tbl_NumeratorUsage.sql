-- Creating table 'tbl_NumeratorUsage'
CREATE TABLE [dbo].[tbl_NumeratorUsage] (
    [ID] uniqueidentifier  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL,
    [DataSet] varchar(256)  NOT NULL
);
GO
-- Creating foreign key on [NumeratorID] in table 'tbl_NumeratorUsage'
ALTER TABLE [dbo].[tbl_NumeratorUsage]
ADD CONSTRAINT [FK_tbl_NumeratorUsage_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_NumeratorUsage'
ALTER TABLE [dbo].[tbl_NumeratorUsage]
ADD CONSTRAINT [PK_tbl_NumeratorUsage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_NumeratorUsage_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_NumeratorUsage_tbl_Numerator]
ON [dbo].[tbl_NumeratorUsage]
    ([NumeratorID]);