-- Creating table 'tbl_EventCategories'
CREATE TABLE [dbo].[tbl_EventCategories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_EventCategories'
ALTER TABLE [dbo].[tbl_EventCategories]
ADD CONSTRAINT [PK_tbl_EventCategories]
    PRIMARY KEY CLUSTERED ([ID] ASC);