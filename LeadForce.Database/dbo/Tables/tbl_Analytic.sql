-- Creating table 'tbl_Analytic'
CREATE TABLE [dbo].[tbl_Analytic] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(512)  NOT NULL,
    [Query] nvarchar(max)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Analytic'
ALTER TABLE [dbo].[tbl_Analytic]
ADD CONSTRAINT [PK_tbl_Analytic]
    PRIMARY KEY CLUSTERED ([ID] ASC);