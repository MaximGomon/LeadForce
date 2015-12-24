-- Creating table 'tbl_Unit'
CREATE TABLE [dbo].[tbl_Unit] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Unit'
ALTER TABLE [dbo].[tbl_Unit]
ADD CONSTRAINT [PK_tbl_Unit]
    PRIMARY KEY CLUSTERED ([ID] ASC);