-- Creating table 'tbl_TaskMembersCount'
CREATE TABLE [dbo].[tbl_TaskMembersCount] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_TaskMembersCount'
ALTER TABLE [dbo].[tbl_TaskMembersCount]
ADD CONSTRAINT [PK_tbl_TaskMembersCount]
    PRIMARY KEY CLUSTERED ([ID] ASC);