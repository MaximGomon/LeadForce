-- Creating table 'tbl_CronJob'
CREATE TABLE [dbo].[tbl_CronJob] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Assembly] nvarchar(256)  NOT NULL,
    [Type] nvarchar(256)  NOT NULL,
    [Period] int  NOT NULL,
    [LastRunAt] datetime  NULL,
    [LastRunStatusID] int  NULL,
    [NextRunPlannedAt] datetime  NULL,
    [ExecutionTime] int  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_CronJob'
ALTER TABLE [dbo].[tbl_CronJob]
ADD CONSTRAINT [PK_tbl_CronJob]
    PRIMARY KEY CLUSTERED ([ID] ASC);