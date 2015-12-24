-- Creating table 'tbl_ActionStatus'
CREATE TABLE [dbo].[tbl_ActionStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ActionStatus'
ALTER TABLE [dbo].[tbl_ActionStatus]
ADD CONSTRAINT [PK_tbl_ActionStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);