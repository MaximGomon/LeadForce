-- Creating table 'tbl_InvoiceInformCatalog'
CREATE TABLE [dbo].[tbl_InvoiceInformCatalog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceInformCatalog'
ALTER TABLE [dbo].[tbl_InvoiceInformCatalog]
ADD CONSTRAINT [PK_tbl_InvoiceInformCatalog]
    PRIMARY KEY CLUSTERED ([ID] ASC);