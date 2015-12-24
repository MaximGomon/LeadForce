-- Creating table 'tbl_SessionSourceRule'
CREATE TABLE [dbo].[tbl_SessionSourceRule] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [SessionRuleTypeID] tinyint  NOT NULL,
    [Pattern] nvarchar(2000)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_SessionSourceRule'
ALTER TABLE [dbo].[tbl_SessionSourceRule]
ADD CONSTRAINT [PK_tbl_SessionSourceRule]
    PRIMARY KEY CLUSTERED ([ID] ASC);