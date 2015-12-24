-- Creating table 'tbl_Formula'
CREATE TABLE [dbo].[tbl_Formula] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_Formula'
ALTER TABLE [dbo].[tbl_Formula]
ADD CONSTRAINT [PK_tbl_Formula]
    PRIMARY KEY CLUSTERED ([ID] ASC);