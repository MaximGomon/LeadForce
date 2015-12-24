-- Creating table 'tbl_ServiceLevelOutOfListServiceContacts'
CREATE TABLE [dbo].[tbl_ServiceLevelOutOfListServiceContacts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelOutOfListServiceContacts'
ALTER TABLE [dbo].[tbl_ServiceLevelOutOfListServiceContacts]
ADD CONSTRAINT [PK_tbl_ServiceLevelOutOfListServiceContacts]
    PRIMARY KEY CLUSTERED ([ID] ASC);