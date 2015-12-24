-- Creating table 'tbl_LogicConditions'
CREATE TABLE [dbo].[tbl_LogicConditions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_LogicConditions'
ALTER TABLE [dbo].[tbl_LogicConditions]
ADD CONSTRAINT [PK_tbl_LogicConditions]
    PRIMARY KEY CLUSTERED ([ID] ASC);