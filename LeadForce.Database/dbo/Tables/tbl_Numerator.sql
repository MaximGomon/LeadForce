-- Creating table 'tbl_Numerator'
CREATE TABLE [dbo].[tbl_Numerator] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Mask] nvarchar(256)  NOT NULL,
    [NumeratorPeriodID] int  NOT NULL
);
GO
-- Creating foreign key on [NumeratorPeriodID] in table 'tbl_Numerator'
ALTER TABLE [dbo].[tbl_Numerator]
ADD CONSTRAINT [FK_tbl_Numerator_tbl_NumeratorPeriod]
    FOREIGN KEY ([NumeratorPeriodID])
    REFERENCES [dbo].[tbl_NumeratorPeriod]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Numerator'
ALTER TABLE [dbo].[tbl_Numerator]
ADD CONSTRAINT [FK_tbl_Numerator_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Numerator'
ALTER TABLE [dbo].[tbl_Numerator]
ADD CONSTRAINT [PK_tbl_Numerator]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Numerator_tbl_NumeratorPeriod'
CREATE INDEX [IX_FK_tbl_Numerator_tbl_NumeratorPeriod]
ON [dbo].[tbl_Numerator]
    ([NumeratorPeriodID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Numerator_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Numerator_tbl_Sites]
ON [dbo].[tbl_Numerator]
    ([SiteID]);