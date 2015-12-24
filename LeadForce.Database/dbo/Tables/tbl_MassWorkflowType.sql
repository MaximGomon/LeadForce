-- Creating table 'tbl_MassWorkflowType'
CREATE TABLE [dbo].[tbl_MassWorkflowType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_MassWorkflowType'
ALTER TABLE [dbo].[tbl_MassWorkflowType]
ADD CONSTRAINT [PK_tbl_MassWorkflowType]
    PRIMARY KEY CLUSTERED ([ID] ASC);