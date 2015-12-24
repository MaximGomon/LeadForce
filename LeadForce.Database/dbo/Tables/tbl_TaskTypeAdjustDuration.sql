-- Creating table 'tbl_TaskTypeAdjustDuration'
CREATE TABLE [dbo].[tbl_TaskTypeAdjustDuration] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_TaskTypeAdjustDuration'
ALTER TABLE [dbo].[tbl_TaskTypeAdjustDuration]
ADD CONSTRAINT [PK_tbl_TaskTypeAdjustDuration]
    PRIMARY KEY CLUSTERED ([ID] ASC);