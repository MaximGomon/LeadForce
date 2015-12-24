-- Creating table 'tbl_RequestStatus'
CREATE TABLE [dbo].[tbl_RequestStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_RequestStatus'
ALTER TABLE [dbo].[tbl_RequestStatus]
ADD CONSTRAINT [PK_tbl_RequestStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);