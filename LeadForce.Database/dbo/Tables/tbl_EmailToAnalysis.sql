-- Creating table 'tbl_EmailToAnalysis'
CREATE TABLE [dbo].[tbl_EmailToAnalysis] (
    [ID] uniqueidentifier  NOT NULL,
    [SourceMonitoringID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [From] nvarchar(255)  NOT NULL,
    [Name] nvarchar(255)  NULL,
    [Subject] nvarchar(255)  NOT NULL,
    [MessageText] nvarchar(max)  NOT NULL,
    [POPMessageID] nvarchar(150)  NULL
);
GO
-- Creating foreign key on [SourceMonitoringID] in table 'tbl_EmailToAnalysis'
ALTER TABLE [dbo].[tbl_EmailToAnalysis]
ADD CONSTRAINT [FK_tbl_EmailToAnalysis_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_EmailToAnalysis'
ALTER TABLE [dbo].[tbl_EmailToAnalysis]
ADD CONSTRAINT [PK_tbl_EmailToAnalysis]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_EmailToAnalysis_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_EmailToAnalysis_tbl_SourceMonitoring]
ON [dbo].[tbl_EmailToAnalysis]
    ([SourceMonitoringID]);