-- Creating table 'tbl_ProductWorkWithComplectation'
CREATE TABLE [dbo].[tbl_ProductWorkWithComplectation] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ProductWorkWithComplectation'
ALTER TABLE [dbo].[tbl_ProductWorkWithComplectation]
ADD CONSTRAINT [PK_tbl_ProductWorkWithComplectation]
    PRIMARY KEY CLUSTERED ([ID] ASC);