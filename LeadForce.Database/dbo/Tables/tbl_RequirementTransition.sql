-- Creating table 'tbl_RequirementTransition'
CREATE TABLE [dbo].[tbl_RequirementTransition] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [InitialRequirementStatusID] uniqueidentifier  NOT NULL,
    [FinalRequirementStatusID] uniqueidentifier  NOT NULL,
    [AllowedAccessProfileID] uniqueidentifier  NULL,
    [IsPortalAllowed] bit  NOT NULL,
    [RequirementTypeID] uniqueidentifier  NULL,
    [IsReviewRequired] bit  NOT NULL
);
GO
-- Creating foreign key on [AllowedAccessProfileID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_AccessProfile]
    FOREIGN KEY ([AllowedAccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [FinalRequirementStatusID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementStatus_Final]
    FOREIGN KEY ([FinalRequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InitialRequirementStatusID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial]
    FOREIGN KEY ([InitialRequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementTypeID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementType]
    FOREIGN KEY ([RequirementTypeID])
    REFERENCES [dbo].[tbl_RequirementType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [PK_tbl_RequirementTransition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_AccessProfile]
ON [dbo].[tbl_RequirementTransition]
    ([AllowedAccessProfileID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_RequirementStatus_Final'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_RequirementStatus_Final]
ON [dbo].[tbl_RequirementTransition]
    ([FinalRequirementStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial]
ON [dbo].[tbl_RequirementTransition]
    ([InitialRequirementStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_RequirementType'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_RequirementType]
ON [dbo].[tbl_RequirementTransition]
    ([RequirementTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_Sites]
ON [dbo].[tbl_RequirementTransition]
    ([SiteID]);