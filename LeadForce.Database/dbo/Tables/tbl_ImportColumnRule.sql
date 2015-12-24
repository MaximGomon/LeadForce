-- Creating table 'tbl_ImportColumnRule'
CREATE TABLE [dbo].[tbl_ImportColumnRule] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [ImportFieldID] uniqueidentifier  NOT NULL,
    [ImportColumnID] uniqueidentifier  NOT NULL,
    [IsRequired] bit  NOT NULL,
    [SQLCode] nvarchar(max)  NULL,
    [ImportFieldDictionaryID] uniqueidentifier  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ImportColumnRule'
ALTER TABLE [dbo].[tbl_ImportColumnRule]
ADD CONSTRAINT [PK_tbl_ImportColumnRule]
    PRIMARY KEY CLUSTERED ([ID] ASC);