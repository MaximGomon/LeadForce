-- Creating table 'tbl_ProductStatus'
CREATE TABLE [dbo].[tbl_ProductStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ProductStatus'
ALTER TABLE [dbo].[tbl_ProductStatus]
ADD CONSTRAINT [PK_tbl_ProductStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);