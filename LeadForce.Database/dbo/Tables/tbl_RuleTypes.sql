-- Creating table 'tbl_RuleTypes'
CREATE TABLE [dbo].[tbl_RuleTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_RuleTypes'
ALTER TABLE [dbo].[tbl_RuleTypes]
ADD CONSTRAINT [PK_tbl_RuleTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);