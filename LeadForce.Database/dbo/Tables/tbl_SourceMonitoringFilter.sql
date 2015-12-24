-- Creating table 'tbl_SourceMonitoringFilter'
CREATE TABLE [dbo].[tbl_SourceMonitoringFilter] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SourceMonitoringID] uniqueidentifier  NOT NULL,
    [SourcePropertyID] int  NOT NULL,
    [Mask] nvarchar(2000)  NOT NULL,
    [MonitoringActionID] int  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_SourceMonitoringFilter'
ALTER TABLE [dbo].[tbl_SourceMonitoringFilter]
ADD CONSTRAINT [FK_tbl_SourceMonitoringFilter_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SourceMonitoringID] in table 'tbl_SourceMonitoringFilter'
ALTER TABLE [dbo].[tbl_SourceMonitoringFilter]
ADD CONSTRAINT [FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SourceMonitoringFilter'
ALTER TABLE [dbo].[tbl_SourceMonitoringFilter]
ADD CONSTRAINT [PK_tbl_SourceMonitoringFilter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoringFilter_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SourceMonitoringFilter_tbl_Sites]
ON [dbo].[tbl_SourceMonitoringFilter]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring]
ON [dbo].[tbl_SourceMonitoringFilter]
    ([SourceMonitoringID]);