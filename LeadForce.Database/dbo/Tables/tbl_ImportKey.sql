-- Creating table 'tbl_ImportKey'
CREATE TABLE [dbo].[tbl_ImportKey] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [TableName] nvarchar(255)  NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [LeadForceID] uniqueidentifier  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ImportKey'
ALTER TABLE [dbo].[tbl_ImportKey]
ADD CONSTRAINT [PK_tbl_ImportKey]
    PRIMARY KEY CLUSTERED ([ID] ASC);