-- Creating table 'tbl_EmailStats'
CREATE TABLE [dbo].[tbl_EmailStats] (
    [ID] uniqueidentifier  NOT NULL,
    [Email] nvarchar(256)  NOT NULL,
    [ReturnCount] int  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_EmailStats'
ALTER TABLE [dbo].[tbl_EmailStats]
ADD CONSTRAINT [PK_tbl_EmailStats]
    PRIMARY KEY CLUSTERED ([ID] ASC);