-- Creating table 'tbl_PriceListType'
CREATE TABLE [dbo].[tbl_PriceListType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PriceListType'
ALTER TABLE [dbo].[tbl_PriceListType]
ADD CONSTRAINT [PK_tbl_PriceListType]
    PRIMARY KEY CLUSTERED ([ID] ASC);