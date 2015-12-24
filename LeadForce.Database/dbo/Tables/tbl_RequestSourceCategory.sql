-- Creating table 'tbl_RequestSourceCategory'
CREATE TABLE [dbo].[tbl_RequestSourceCategory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_RequestSourceCategory'
ALTER TABLE [dbo].[tbl_RequestSourceCategory]
ADD CONSTRAINT [PK_tbl_RequestSourceCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);