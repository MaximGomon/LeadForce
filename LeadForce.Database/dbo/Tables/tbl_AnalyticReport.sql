-- Creating table 'tbl_AnalyticReport'
CREATE TABLE [dbo].[tbl_AnalyticReport] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Description] nvarchar(1024)  NULL,
    [ModuleID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [AnalyticID] in table 'tbl_AnalyticReport'
ALTER TABLE [dbo].[tbl_AnalyticReport]
ADD CONSTRAINT [FK_tbl_AnalyticReport_tbl_Analytic]
    FOREIGN KEY ([AnalyticID])
    REFERENCES [dbo].[tbl_Analytic]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleID] in table 'tbl_AnalyticReport'
ALTER TABLE [dbo].[tbl_AnalyticReport]
ADD CONSTRAINT [FK_tbl_AnalyticReport_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AnalyticReport'
ALTER TABLE [dbo].[tbl_AnalyticReport]
ADD CONSTRAINT [PK_tbl_AnalyticReport]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReport_tbl_Analytic'
CREATE INDEX [IX_FK_tbl_AnalyticReport_tbl_Analytic]
ON [dbo].[tbl_AnalyticReport]
    ([AnalyticID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReport_tbl_Module'
CREATE INDEX [IX_FK_tbl_AnalyticReport_tbl_Module]
ON [dbo].[tbl_AnalyticReport]
    ([ModuleID]);