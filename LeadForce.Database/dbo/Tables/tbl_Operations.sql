-- Creating table 'tbl_Operations'
CREATE TABLE [dbo].[tbl_Operations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Operations'
ALTER TABLE [dbo].[tbl_Operations]
ADD CONSTRAINT [PK_tbl_Operations]
    PRIMARY KEY CLUSTERED ([ID] ASC);