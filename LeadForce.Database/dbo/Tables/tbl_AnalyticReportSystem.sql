-- Creating table 'tbl_AnalyticReportSystem'
CREATE TABLE [dbo].[tbl_AnalyticReportSystem] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticReportID] uniqueidentifier  NOT NULL,
    [AnalyticAxisID] uniqueidentifier  NOT NULL,
    [AxisTypeID] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [AnalyticAxisID] in table 'tbl_AnalyticReportSystem'
ALTER TABLE [dbo].[tbl_AnalyticReportSystem]
ADD CONSTRAINT [FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis]
    FOREIGN KEY ([AnalyticAxisID])
    REFERENCES [dbo].[tbl_AnalyticAxis]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [AnalyticReportID] in table 'tbl_AnalyticReportSystem'
ALTER TABLE [dbo].[tbl_AnalyticReportSystem]
ADD CONSTRAINT [FK_tbl_AnalyticReportSystem_tbl_AnalyticReport]
    FOREIGN KEY ([AnalyticReportID])
    REFERENCES [dbo].[tbl_AnalyticReport]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AnalyticReportSystem'
ALTER TABLE [dbo].[tbl_AnalyticReportSystem]
ADD CONSTRAINT [PK_tbl_AnalyticReportSystem]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis'
CREATE INDEX [IX_FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis]
ON [dbo].[tbl_AnalyticReportSystem]
    ([AnalyticAxisID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportSystem_tbl_AnalyticReport'
CREATE INDEX [IX_FK_tbl_AnalyticReportSystem_tbl_AnalyticReport]
ON [dbo].[tbl_AnalyticReportSystem]
    ([AnalyticReportID]);