-- Creating table 'tbl_Direction'
CREATE TABLE [dbo].[tbl_Direction] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Direction'
ALTER TABLE [dbo].[tbl_Direction]
ADD CONSTRAINT [PK_tbl_Direction]
    PRIMARY KEY CLUSTERED ([ID] ASC);