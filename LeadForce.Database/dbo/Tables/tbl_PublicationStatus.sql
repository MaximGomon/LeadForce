-- Creating table 'tbl_PublicationStatus'
CREATE TABLE [dbo].[tbl_PublicationStatus] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [isFirst] bit  NULL,
    [isActive] bit  NULL,
    [isLast] bit  NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_PublicationStatus'
ALTER TABLE [dbo].[tbl_PublicationStatus]
ADD CONSTRAINT [PK_tbl_PublicationStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);