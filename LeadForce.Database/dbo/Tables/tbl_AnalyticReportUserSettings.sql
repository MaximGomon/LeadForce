-- Creating table 'tbl_AnalyticReportUserSettings'
CREATE TABLE [dbo].[tbl_AnalyticReportUserSettings] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [AnalyticReportID] uniqueidentifier  NOT NULL,
    [AxisToBuildID] uniqueidentifier  NULL,
    [DataSetValues] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [AnalyticReportID] in table 'tbl_AnalyticReportUserSettings'
ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings]
ADD CONSTRAINT [FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport]
    FOREIGN KEY ([AnalyticReportID])
    REFERENCES [dbo].[tbl_AnalyticReport]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UserID] in table 'tbl_AnalyticReportUserSettings'
ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings]
ADD CONSTRAINT [FK_tbl_AnalyticReportUserSetting_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AnalyticReportUserSettings'
ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings]
ADD CONSTRAINT [PK_tbl_AnalyticReportUserSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport'
CREATE INDEX [IX_FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport]
ON [dbo].[tbl_AnalyticReportUserSettings]
    ([AnalyticReportID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportUserSetting_tbl_User'
CREATE INDEX [IX_FK_tbl_AnalyticReportUserSetting_tbl_User]
ON [dbo].[tbl_AnalyticReportUserSettings]
    ([UserID]);