-- Creating table 'tbl_Status'
CREATE TABLE [dbo].[tbl_Status] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Status'
ALTER TABLE [dbo].[tbl_Status]
ADD CONSTRAINT [PK_tbl_Status]
    PRIMARY KEY CLUSTERED ([ID] ASC);