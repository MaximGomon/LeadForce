-- Creating table 'tbl_TaskTypePaymentScheme'
CREATE TABLE [dbo].[tbl_TaskTypePaymentScheme] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_TaskTypePaymentScheme'
ALTER TABLE [dbo].[tbl_TaskTypePaymentScheme]
ADD CONSTRAINT [PK_tbl_TaskTypePaymentScheme]
    PRIMARY KEY CLUSTERED ([ID] ASC);