-- Creating table 'tbl_PaymentType'
CREATE TABLE [dbo].[tbl_PaymentType] (
    [ID] int  NOT NULL,
    [Title] nvarchar(250)  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PaymentType'
ALTER TABLE [dbo].[tbl_PaymentType]
ADD CONSTRAINT [PK_tbl_PaymentType]
    PRIMARY KEY CLUSTERED ([ID] ASC);