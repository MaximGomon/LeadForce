-- Creating table 'tbl_TaskTypeCategory'
CREATE TABLE [dbo].[tbl_TaskTypeCategory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_TaskTypeCategory'
ALTER TABLE [dbo].[tbl_TaskTypeCategory]
ADD CONSTRAINT [PK_tbl_TaskTypeCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);