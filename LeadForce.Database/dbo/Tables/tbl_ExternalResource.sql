-- Creating table 'tbl_ExternalResource'
CREATE TABLE [dbo].[tbl_ExternalResource] (
    [ID] uniqueidentifier  NOT NULL,
    [DestinationID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ResourcePlaceID] int  NOT NULL,
    [ExternalResourceTypeID] int  NOT NULL,
    [File] nvarchar(256)  NULL,
    [Text] nvarchar(max)  NULL,
    [Url] nvarchar(1024)  NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ExternalResource'
ALTER TABLE [dbo].[tbl_ExternalResource]
ADD CONSTRAINT [PK_tbl_ExternalResource]
    PRIMARY KEY CLUSTERED ([ID] ASC);