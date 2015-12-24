-- Creating table 'tbl_ImportField'
CREATE TABLE [dbo].[tbl_ImportField] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportTable] int  NOT NULL,
    [FieldTitle] nvarchar(255)  NOT NULL,
    [TableName] varchar(50)  NULL,
    [FieldName] varchar(50)  NULL,
    [IsDictionary] bit  NOT NULL,
    [IsAddress] bit  NOT NULL,
    [Order] tinyint  NOT NULL,
    [ParentTableName] nvarchar(255)  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ImportField'
ALTER TABLE [dbo].[tbl_ImportField]
ADD CONSTRAINT [PK_tbl_ImportField]
    PRIMARY KEY CLUSTERED ([ID] ASC);