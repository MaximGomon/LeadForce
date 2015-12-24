-- Creating table 'tbl_ObjectTypes'
CREATE TABLE [dbo].[tbl_ObjectTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ObjectTypes'
ALTER TABLE [dbo].[tbl_ObjectTypes]
ADD CONSTRAINT [PK_tbl_ObjectTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);