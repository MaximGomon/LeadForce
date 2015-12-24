-- Creating table 'tbl_ReadyToSell'
CREATE TABLE [dbo].[tbl_ReadyToSell] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [MinScore] int  NOT NULL,
    [MaxScore] int  NOT NULL,
    [Image] nvarchar(250)  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ReadyToSell'
ALTER TABLE [dbo].[tbl_ReadyToSell]
ADD CONSTRAINT [PK_tbl_ReadyToSell]
    PRIMARY KEY CLUSTERED ([ID] ASC);