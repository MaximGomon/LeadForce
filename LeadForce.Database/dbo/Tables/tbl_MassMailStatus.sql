-- Creating table 'tbl_MassMailStatus'
CREATE TABLE [dbo].[tbl_MassMailStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_MassMailStatus'
ALTER TABLE [dbo].[tbl_MassMailStatus]
ADD CONSTRAINT [PK_tbl_MassMailStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);