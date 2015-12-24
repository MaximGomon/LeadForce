-- Creating table 'tbl_PriceListStatus'
CREATE TABLE [dbo].[tbl_PriceListStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PriceListStatus'
ALTER TABLE [dbo].[tbl_PriceListStatus]
ADD CONSTRAINT [PK_tbl_PriceListStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);