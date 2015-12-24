-- Creating table 'tbl_ServiceLevelIncludeToInform'
CREATE TABLE [dbo].[tbl_ServiceLevelIncludeToInform] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelIncludeToInform'
ALTER TABLE [dbo].[tbl_ServiceLevelIncludeToInform]
ADD CONSTRAINT [PK_tbl_ServiceLevelIncludeToInform]
    PRIMARY KEY CLUSTERED ([ID] ASC);