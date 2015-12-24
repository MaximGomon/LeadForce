-- Creating table 'tbl_ActivityTypes'
CREATE TABLE [dbo].[tbl_ActivityTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ActivityTypes'
ALTER TABLE [dbo].[tbl_ActivityTypes]
ADD CONSTRAINT [PK_tbl_ActivityTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);