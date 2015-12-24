-- Creating table 'tbl_RequirementStatus'
CREATE TABLE [dbo].[tbl_RequirementStatus] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [IsLast] bit  NOT NULL,
    [ServiceLevelRoleID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ServiceLevelRoleID] in table 'tbl_RequirementStatus'
ALTER TABLE [dbo].[tbl_RequirementStatus]
ADD CONSTRAINT [FK_tbl_RequirementStatus_tbl_ServiceLevelRole]
    FOREIGN KEY ([ServiceLevelRoleID])
    REFERENCES [dbo].[tbl_ServiceLevelRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_RequirementStatus'
ALTER TABLE [dbo].[tbl_RequirementStatus]
ADD CONSTRAINT [FK_tbl_RequirementStatus_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementStatus'
ALTER TABLE [dbo].[tbl_RequirementStatus]
ADD CONSTRAINT [PK_tbl_RequirementStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementStatus_tbl_ServiceLevelRole'
CREATE INDEX [IX_FK_tbl_RequirementStatus_tbl_ServiceLevelRole]
ON [dbo].[tbl_RequirementStatus]
    ([ServiceLevelRoleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementStatus_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementStatus_tbl_Sites]
ON [dbo].[tbl_RequirementStatus]
    ([SiteID]);