-- Creating table 'tbl_ContactRoleType'
CREATE TABLE [dbo].[tbl_ContactRoleType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ContactRoleType'
ALTER TABLE [dbo].[tbl_ContactRoleType]
ADD CONSTRAINT [PK_tbl_ContactRoleType]
    PRIMARY KEY CLUSTERED ([ID] ASC);