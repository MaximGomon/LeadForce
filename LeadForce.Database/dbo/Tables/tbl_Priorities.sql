-- Creating table 'tbl_Priorities'
CREATE TABLE [dbo].[tbl_Priorities] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [MinScore] int  NOT NULL,
    [MaxScore] int  NOT NULL,
    [Image] nvarchar(250)  NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_Priorities'
ALTER TABLE [dbo].[tbl_Priorities]
ADD CONSTRAINT [FK_tbl_Priorities_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Priorities'
ALTER TABLE [dbo].[tbl_Priorities]
ADD CONSTRAINT [PK_tbl_Priorities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Priorities_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Priorities_tbl_Sites]
ON [dbo].[tbl_Priorities]
    ([SiteID]);