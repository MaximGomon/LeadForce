-- Creating table 'tbl_CurrencyCourse'
CREATE TABLE [dbo].[tbl_CurrencyCourse] (
    [ID] uniqueidentifier  NOT NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Nominal] int  NOT NULL,
    [Course] decimal(19,4)  NOT NULL,
    [InternalCourse] decimal(19,4)  NULL
);
GO
-- Creating foreign key on [CurrencyID] in table 'tbl_CurrencyCourse'
ALTER TABLE [dbo].[tbl_CurrencyCourse]
ADD CONSTRAINT [FK_tbl_CurrencyCourse_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_CurrencyCourse'
ALTER TABLE [dbo].[tbl_CurrencyCourse]
ADD CONSTRAINT [PK_tbl_CurrencyCourse]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CurrencyCourse_tbl_Currency'
CREATE INDEX [IX_FK_tbl_CurrencyCourse_tbl_Currency]
ON [dbo].[tbl_CurrencyCourse]
    ([CurrencyID]);