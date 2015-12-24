-- Creating table 'tbl_PublicationKind'
CREATE TABLE [dbo].[tbl_PublicationKind] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PublicationKind'
ALTER TABLE [dbo].[tbl_PublicationKind]
ADD CONSTRAINT [PK_tbl_PublicationKind]
    PRIMARY KEY CLUSTERED ([ID] ASC);