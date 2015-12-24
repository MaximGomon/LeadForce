-- Creating table 'tbl_AnalyticAxisFilterValues'
CREATE TABLE [dbo].[tbl_AnalyticAxisFilterValues] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticAxisID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ColumnName] varchar(100)  NULL,
    [Value] nvarchar(256)  NULL,
    [DisplayOrder] int  NOT NULL,
    [FilterOperatorID] int  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [FilterType] int  NOT NULL,
    [Query] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [AnalyticAxisID] in table 'tbl_AnalyticAxisFilterValues'
ALTER TABLE [dbo].[tbl_AnalyticAxisFilterValues]
ADD CONSTRAINT [FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis]
    FOREIGN KEY ([AnalyticAxisID])
    REFERENCES [dbo].[tbl_AnalyticAxis]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID], [AnalyticAxisID], [Title], [DisplayOrder], [FilterOperatorID], [IsDefault], [FilterType] in table 'tbl_AnalyticAxisFilterValues'
ALTER TABLE [dbo].[tbl_AnalyticAxisFilterValues]
ADD CONSTRAINT [PK_tbl_AnalyticAxisFilterValues]
    PRIMARY KEY CLUSTERED ([ID], [AnalyticAxisID], [Title], [DisplayOrder], [FilterOperatorID], [IsDefault], [FilterType] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis'
CREATE INDEX [IX_FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis]
ON [dbo].[tbl_AnalyticAxisFilterValues]
    ([AnalyticAxisID]);