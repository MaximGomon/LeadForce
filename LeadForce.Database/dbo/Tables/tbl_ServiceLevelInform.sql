-- Creating table 'tbl_ServiceLevelInform'
CREATE TABLE [dbo].[tbl_ServiceLevelInform] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelInform'
ALTER TABLE [dbo].[tbl_ServiceLevelInform]
ADD CONSTRAINT [PK_tbl_ServiceLevelInform]
    PRIMARY KEY CLUSTERED ([ID] ASC);