-- Creating table 'tbl_ServiceLevelRole'
CREATE TABLE [dbo].[tbl_ServiceLevelRole] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_ServiceLevelRole'
ALTER TABLE [dbo].[tbl_ServiceLevelRole]
ADD CONSTRAINT [FK_tbl_ServiceLevelRole_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelRole'
ALTER TABLE [dbo].[tbl_ServiceLevelRole]
ADD CONSTRAINT [PK_tbl_ServiceLevelRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelRole_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ServiceLevelRole_tbl_Sites]
ON [dbo].[tbl_ServiceLevelRole]
    ([SiteID]);