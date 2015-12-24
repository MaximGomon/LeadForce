-- Creating table 'tbl_AnalyticAxis'
CREATE TABLE [dbo].[tbl_AnalyticAxis] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [SystemName] nvarchar(256)  NOT NULL,
    [AxisRoleID] int  NOT NULL,
    [DataSet] varchar(256)  NULL,
    [Query] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [AnalyticID] in table 'tbl_AnalyticAxis'
ALTER TABLE [dbo].[tbl_AnalyticAxis]
ADD CONSTRAINT [FK_tbl_AnalyticAxis_tbl_Analytic]
    FOREIGN KEY ([AnalyticID])
    REFERENCES [dbo].[tbl_Analytic]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AnalyticAxis'
ALTER TABLE [dbo].[tbl_AnalyticAxis]
ADD CONSTRAINT [PK_tbl_AnalyticAxis]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticAxis_tbl_Analytic'
CREATE INDEX [IX_FK_tbl_AnalyticAxis_tbl_Analytic]
ON [dbo].[tbl_AnalyticAxis]
    ([AnalyticID]);