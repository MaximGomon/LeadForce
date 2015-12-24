-- Creating table 'tbl_UserSettings'
CREATE TABLE [dbo].[tbl_UserSettings] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [ClassName] nvarchar(255)  NULL,
    [UserSettings] nvarchar(max)  NULL,
    [ShowGroupPanel] bit  NOT NULL,
    [ShowFilterPanel] bit  NOT NULL,
    [ShowAlternativeControl] bit  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_UserSettings'
ALTER TABLE [dbo].[tbl_UserSettings]
ADD CONSTRAINT [PK_tbl_UserSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);