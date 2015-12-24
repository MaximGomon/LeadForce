-- Creating table 'tbl_PublicationAccessComment'
CREATE TABLE [dbo].[tbl_PublicationAccessComment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PublicationAccessComment'
ALTER TABLE [dbo].[tbl_PublicationAccessComment]
ADD CONSTRAINT [PK_tbl_PublicationAccessComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);