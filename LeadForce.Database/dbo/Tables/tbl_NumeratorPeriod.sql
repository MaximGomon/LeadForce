-- Creating table 'tbl_NumeratorPeriod'
CREATE TABLE [dbo].[tbl_NumeratorPeriod] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_NumeratorPeriod'
ALTER TABLE [dbo].[tbl_NumeratorPeriod]
ADD CONSTRAINT [PK_tbl_NumeratorPeriod]
    PRIMARY KEY CLUSTERED ([ID] ASC);