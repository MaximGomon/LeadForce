-- Creating table 'tbl_ViewTypes'
CREATE TABLE [dbo].[tbl_ViewTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ViewTypes'
ALTER TABLE [dbo].[tbl_ViewTypes]
ADD CONSTRAINT [PK_tbl_ViewTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);