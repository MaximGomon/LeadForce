-- Creating table 'tbl_InvoiceStatus'
CREATE TABLE [dbo].[tbl_InvoiceStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceStatus'
ALTER TABLE [dbo].[tbl_InvoiceStatus]
ADD CONSTRAINT [PK_tbl_InvoiceStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);