-- Creating table 'tbl_Module'
CREATE TABLE [dbo].[tbl_Module] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [TableName] varchar(50)  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Module'
ALTER TABLE [dbo].[tbl_Module]
ADD CONSTRAINT [PK_tbl_Module]
    PRIMARY KEY CLUSTERED ([ID] ASC);