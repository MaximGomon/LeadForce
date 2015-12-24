-- Creating table 'tbl_EmailActions'
CREATE TABLE [dbo].[tbl_EmailActions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_EmailActions'
ALTER TABLE [dbo].[tbl_EmailActions]
ADD CONSTRAINT [PK_tbl_EmailActions]
    PRIMARY KEY CLUSTERED ([ID] ASC);