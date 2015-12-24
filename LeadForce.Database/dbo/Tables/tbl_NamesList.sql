-- Creating table 'tbl_NamesList'
CREATE TABLE [dbo].[tbl_NamesList] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [PatronymicMask] nvarchar(255)  NULL,
    [Gender] nchar(1)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_NamesList'
ALTER TABLE [dbo].[tbl_NamesList]
ADD CONSTRAINT [PK_tbl_NamesList]
    PRIMARY KEY CLUSTERED ([ID] ASC);