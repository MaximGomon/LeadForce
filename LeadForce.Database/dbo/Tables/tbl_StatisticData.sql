-- Creating table 'tbl_StatisticData'
CREATE TABLE [dbo].[tbl_StatisticData] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Code] varchar(256)  NOT NULL,
    [Value] decimal(18,2)  NOT NULL,
    [RecalculateDate] datetime  NULL,
    [PreviousValue] decimal(18,2)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_StatisticData'
ALTER TABLE [dbo].[tbl_StatisticData]
ADD CONSTRAINT [FK_tbl_StatisticData_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_StatisticData'
ALTER TABLE [dbo].[tbl_StatisticData]
ADD CONSTRAINT [PK_tbl_StatisticData]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_StatisticData_tbl_Sites'
CREATE INDEX [IX_FK_tbl_StatisticData_tbl_Sites]
ON [dbo].[tbl_StatisticData]
    ([SiteID]);