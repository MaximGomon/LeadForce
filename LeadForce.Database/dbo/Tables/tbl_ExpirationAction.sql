-- Creating table 'tbl_ExpirationAction'
CREATE TABLE [dbo].[tbl_ExpirationAction] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ExpirationAction'
ALTER TABLE [dbo].[tbl_ExpirationAction]
ADD CONSTRAINT [PK_tbl_ExpirationAction]
    PRIMARY KEY CLUSTERED ([ID] ASC);