-- Creating table 'tbl_PublicationAccessRecord'
CREATE TABLE [dbo].[tbl_PublicationAccessRecord] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PublicationAccessRecord'
ALTER TABLE [dbo].[tbl_PublicationAccessRecord]
ADD CONSTRAINT [PK_tbl_PublicationAccessRecord]
    PRIMARY KEY CLUSTERED ([ID] ASC);