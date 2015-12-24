-- Creating table 'tbl_ImportColumn'
CREATE TABLE [dbo].[tbl_ImportColumn] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Source] nvarchar(255)  NOT NULL,
    [SystemName] nvarchar(255)  NOT NULL,
    [PrimaryKey] bit  NOT NULL,
    [SecondaryKey] bit  NOT NULL,
    [Order] tinyint  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ImportColumn'
ALTER TABLE [dbo].[tbl_ImportColumn]
ADD CONSTRAINT [PK_tbl_ImportColumn]
    PRIMARY KEY CLUSTERED ([ID] ASC);