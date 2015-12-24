-- Creating table 'tbl_ServiceLevelInformComment'
CREATE TABLE [dbo].[tbl_ServiceLevelInformComment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelInformComment'
ALTER TABLE [dbo].[tbl_ServiceLevelInformComment]
ADD CONSTRAINT [PK_tbl_ServiceLevelInformComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);