-- Creating table 'tbl_OrderStatus'
CREATE TABLE [dbo].[tbl_OrderStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_OrderStatus'
ALTER TABLE [dbo].[tbl_OrderStatus]
ADD CONSTRAINT [PK_tbl_OrderStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);