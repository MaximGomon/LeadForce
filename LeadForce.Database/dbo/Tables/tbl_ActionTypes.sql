-- Creating table 'tbl_ActionTypes'
CREATE TABLE [dbo].[tbl_ActionTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ActionTypes'
ALTER TABLE [dbo].[tbl_ActionTypes]
ADD CONSTRAINT [PK_tbl_ActionTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);