-- Creating table 'tbl_Filters'
CREATE TABLE [dbo].[tbl_Filters] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ClassName] nvarchar(255)  NULL,
    [Expressions] nvarchar(max)  NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Filters'
ALTER TABLE [dbo].[tbl_Filters]
ADD CONSTRAINT [PK_tbl_Filters]
    PRIMARY KEY CLUSTERED ([ID] ASC);