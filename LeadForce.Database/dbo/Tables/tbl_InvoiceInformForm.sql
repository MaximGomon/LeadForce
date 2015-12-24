-- Creating table 'tbl_InvoiceInformForm'
CREATE TABLE [dbo].[tbl_InvoiceInformForm] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_InvoiceInformForm'
ALTER TABLE [dbo].[tbl_InvoiceInformForm]
ADD CONSTRAINT [PK_tbl_InvoiceInformForm]
    PRIMARY KEY CLUSTERED ([ID] ASC);