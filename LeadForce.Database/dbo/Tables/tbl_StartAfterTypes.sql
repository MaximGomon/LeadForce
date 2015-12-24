-- Creating table 'tbl_StartAfterTypes'
CREATE TABLE [dbo].[tbl_StartAfterTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_StartAfterTypes'
ALTER TABLE [dbo].[tbl_StartAfterTypes]
ADD CONSTRAINT [PK_tbl_StartAfterTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);