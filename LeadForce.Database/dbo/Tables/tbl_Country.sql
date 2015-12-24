-- Creating table 'tbl_Country'
CREATE TABLE [dbo].[tbl_Country] (
    [ImportID] int  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Code] nchar(2)  NOT NULL,
    [ID] uniqueidentifier  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Country'
ALTER TABLE [dbo].[tbl_Country]
ADD CONSTRAINT [PK_tbl_Country]
    PRIMARY KEY CLUSTERED ([ID] ASC);