-- Creating table 'tbl_ImportFieldDictionary'
CREATE TABLE [dbo].[tbl_ImportFieldDictionary] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportFieldID] uniqueidentifier  NOT NULL,
    [TableName] varchar(50)  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Order] tinyint  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ImportFieldDictionary'
ALTER TABLE [dbo].[tbl_ImportFieldDictionary]
ADD CONSTRAINT [PK_tbl_ImportFieldDictionary]
    PRIMARY KEY CLUSTERED ([ID] ASC);