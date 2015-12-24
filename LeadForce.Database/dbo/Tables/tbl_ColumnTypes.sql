-- Creating table 'tbl_ColumnTypes'
CREATE TABLE [dbo].[tbl_ColumnTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(25)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ColumnTypes'
ALTER TABLE [dbo].[tbl_ColumnTypes]
ADD CONSTRAINT [PK_tbl_ColumnTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);